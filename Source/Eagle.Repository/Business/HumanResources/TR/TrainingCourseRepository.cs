using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;
using Eagle.Model;

namespace Eagle.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingCourseRepository
    {
        public EntityDataContext context { get; set; }

        public TrainingCourseRepository(EntityDataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool CheckExist(string code, out string errorMessage)
        {
            try
            {
                var result = this.context.LS_tblTrainingCourse.FirstOrDefault(p => p.LSTrainingCourseCode == code);
                errorMessage = String.Empty;
                return (result != null);
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public LS_tblTrainingCourse Find(int id, out string errorMessage)
        {
            try
            {
                var result = this.context.LS_tblTrainingCourse.Find(id);
                errorMessage = String.Empty;
                return result;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public TrainingCourseViewModel FindForEdit(int id, out string errorMessage)
        {
            try
            {                
                var result = from course in context.LS_tblTrainingCourse
                             join code in context.LS_tblTrainingCode on course.LSTrainingCodeID equals code.LSTrainingCodeID into CourseCode
                             from code in CourseCode.DefaultIfEmpty()

                             join category in context.LS_tblTrainingCategory on course.LSTrainingCategoryID equals category.LSTrainingCategoryID into CourseCategory
                             from category in CourseCategory.DefaultIfEmpty()

                             join provider in context.LS_tblTrainingProvider on course.LSTrainingProviderID equals provider.LSTrainingProviderID into CourseProvider
                             from provider in CourseProvider.DefaultIfEmpty()

                             where course.LSTrainingCourseID == id
                             select new TrainingCourseViewModel()
                             {
                                 LSTrainingCourseID = course.LSTrainingCourseID,
                                 LSTrainingCourseCode = course.LSTrainingCourseCode,
                                 Name = course.Name,
                                 LearningObjectives = course.LearningObjectives,
                                 LSTrainingCodeID = course.LSTrainingCodeID,
                                 LSTrainingCategoryID = course.LSTrainingCategoryID,
                                 LSTrainingProviderID = course.LSTrainingProviderID,
                                 Rank = course.Rank,
                                 Used = course.Used,
                                 Note = course.Note,
                                 TrainingCodeName = code.Name,
                                 TrainingCategoryName = category.Name,
                                 TrainingProviderName = provider.Name,
                                 LSTrainingCodeIDAllowNull = course.LSTrainingCodeID,
                                 LSTrainingCategoryIDAllowNull = course.LSTrainingCategoryID
                             };
                errorMessage = String.Empty;
                return result.FirstOrDefault();                
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<TrainingCourseViewModel> GetAll(out string errorMessage)
        {
            try
            {                
                var result = from course in context.LS_tblTrainingCourse
                             join code in context.LS_tblTrainingCode on course.LSTrainingCodeID equals code.LSTrainingCodeID into CourseCode
                             from code in CourseCode.DefaultIfEmpty()

                             join category in context.LS_tblTrainingCategory on course.LSTrainingCategoryID equals category.LSTrainingCategoryID into CourseCategory
                             from category in CourseCategory.DefaultIfEmpty()

                             join provider in context.LS_tblTrainingProvider on course.LSTrainingProviderID equals provider.LSTrainingProviderID into CourseProvider
                             from provider in CourseProvider.DefaultIfEmpty()

                             select new TrainingCourseViewModel()
                             {
                                 LSTrainingCourseID = course.LSTrainingCourseID,
                                 LSTrainingCourseCode = course.LSTrainingCourseCode,
                                 Name = course.Name,
                                 LearningObjectives = course.LearningObjectives,
                                 LSTrainingCodeID = course.LSTrainingCodeID,
                                 LSTrainingCategoryID = course.LSTrainingCategoryID,
                                 LSTrainingProviderID = course.LSTrainingProviderID,
                                 Rank = course.Rank,
                                 Used = course.Used,
                                 Note = course.Note,
                                 TrainingCodeName = code.Name,
                                 TrainingCategoryName = category.Name,
                                 TrainingProviderName = provider.Name
                             };
                errorMessage = String.Empty;
                return result.ToList();
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Add(LS_tblTrainingCourse model, out string errorMessage)
        {
            try
            {
                this.context.Entry(model).State = System.Data.Entity.EntityState.Added;
                this.context.SaveChanges();
                errorMessage = String.Empty;
                return true;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Update(LS_tblTrainingCourse model, out string errorMessage)
        {
            try
            {
                var result = this.Find(model.LSTrainingCourseID, out errorMessage);
                if (result == null)
                {
                    return false;
                }
                result.LSTrainingCourseCode = model.LSTrainingCourseCode;
                result.Name = model.Name;
                result.Rank = model.Rank;
                result.Note = model.Note;
                result.Used = model.Used;

                result.LearningObjectives = model.LearningObjectives;
                result.LSTrainingCodeID = model.LSTrainingCodeID;
                result.LSTrainingCategoryID = model.LSTrainingCategoryID;
                result.LSTrainingProviderID = model.LSTrainingProviderID;
                
                this.context.Entry(result).State = System.Data.Entity.EntityState.Modified;
                this.context.SaveChanges();
                errorMessage = String.Empty;
                return true;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Delete(int id, out string errorMessage)
        {
            try
            {
                var result = this.Find(id, out errorMessage);
                if (result == null)
                {
                    return false;
                }
                this.context.Entry(result).State = System.Data.Entity.EntityState.Deleted;
                this.context.SaveChanges();
                errorMessage = String.Empty;
                return true;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return false;
            }
        }

        #region Bind DropdownList Training Course
        private List<AutoCompleteViewModel> GetDropdownList(string searchTerm, int? LSTrainingCodeID)
        {
            List<AutoCompleteViewModel> ret = new List<AutoCompleteViewModel>();
            if (!LSTrainingCodeID.HasValue)
            {
                AutoCompleteViewModel p = new AutoCompleteViewModel()
                {
                    id = "0",
                    name = Eagle.Resource.LanguageResource.PleaseInputTrainingCourse,
                    text = String.Empty
                };
                ret.Add(p);
                return ret;
            }
            var list = this.context.LS_tblTrainingCourse.Where(p => p.LSTrainingCodeID == LSTrainingCodeID);
            var tmp = list.Where(c => c.Name.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSTrainingCourseID,
                               name = c.Name,
                               text = c.Name
                           });
            
            foreach (var item in tmp)
            {
                AutoCompleteViewModel p = new AutoCompleteViewModel()
                {
                    id = item.id.ToString(),
                    name = item.name,
                    text = item.name
                };
                ret.Add(p);
            }
            return ret;
        }

        public List<AutoCompleteViewModel> ListDropdown(string searchTerm, int? LSTrainingCodeID, int pageSize, int pageNume)
        {
            return GetDropdownList(searchTerm, LSTrainingCodeID).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
        }
        #endregion
    }
}
