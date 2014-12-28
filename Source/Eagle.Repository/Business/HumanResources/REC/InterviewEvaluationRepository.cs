using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Repository
{
    public class InterviewEvaluationRepository
    {
        public EntityDataContext context { get; set; }
        public InterviewEvaluationRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<REC_sprptInterviewEvaluation_Result> GetResults(int? ProjectID, int? ProjectTournamentID, int LanguageId)
        {
            List<REC_sprptInterviewEvaluation_Result> ret = new List<REC_sprptInterviewEvaluation_Result>();
            //Lấy dữ liệu thô nhiều dòng từ csdl
            var results = context.REC_sprptInterviewEvaluation(ProjectID, ProjectTournamentID, LanguageId).ToList();
            if (results != null && results.Count > 0)
            {
                int seq = 0;
                int tmpCandidateID = 0;
                int maxTournament = 5;
                try
                {
                    maxTournament = context.REC_tblProjectTournament.Where(p => p.ProjectID == ProjectID).Max(p => p.SEQ);
                }
                catch{}
                #region - thêm thông tin
                foreach (var item in results)
                {
                    //Nếu chưa có trong list thì thêm mới
                    if (item.CandidateID != tmpCandidateID)
                    {
                        seq++;
                        var modelAdd = new REC_sprptInterviewEvaluation_Result
                        {
                            CandidateID = item.CandidateID,
                            Seq = seq,
                            KQ = item.KQ,
                            ProjectCode = item.ProjectCode,
                            CandidateName = item.CandidateName,
                            Result = item.Result,
                            Tournament = item.Tournament,
                            ProjectID = item.ProjectID,
                            Status = Eagle.Resource.LanguageResource.Pass
                        };
                        //Nếu kết quả = flase => status = Fail
                        if (modelAdd.Result == 0)
                        {
                            modelAdd.Status = Eagle.Resource.LanguageResource.Fail;
                        }

                        ret.Add(modelAdd);
                        tmpCandidateID = item.CandidateID;
                    }
                    else
                    {
                        //Nếu đã có trong list thì cập nhật lại các thông tin sau
                        var tmpModel = ret.Where(p => p.CandidateID == tmpCandidateID).FirstOrDefault();
                        
                        if (tmpModel != null)
                        {
                            //Cập nhật kết quả phỏng vấn (KQ)
                            tmpModel.KQ += ", " + item.KQ;
                            //Cập nhật vòng phỏng vấn hiện tại (Tournament)
                            tmpModel.Tournament = item.Tournament;
                            //Nếu kết quả là false thì toàn bộ cuộc phỏng vấn Result = Fail
                            if (item.Result == 0)
                            {
                                tmpModel.Status = Eagle.Resource.LanguageResource.Fail;
                                tmpModel.Result = 0;
                            }
                        }
                    }
                }
                #endregion
                #region - Cập nhật thông tin Panding
                var updateList = ret.Where(p => (p.Tournament != maxTournament && p.Result == 1) || p.Tournament == 0).ToList();
                if (updateList != null && updateList.Count > 0)
                {
                    if (ProjectTournamentID == 0)
                    {
                        foreach (var item in updateList)
                        {
                            item.Status = Eagle.Resource.LanguageResource.Pending;
                        }
                    }
                    else
                    {
                        foreach (var item in updateList)
                        {
                            if (item.Tournament == 0)
                            {
                                item.Status = Eagle.Resource.LanguageResource.Pending;
                            }
                        }
                    }
                }
                #endregion
            }
            return ret;
        }
    }
}
