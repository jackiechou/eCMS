using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    /*
        id: 999,                        =>Nếu có chung ID có nghĩa là lịch nhiều ngày có thể kéo thả cùng nhau
        title: 'Repeating Event',       =>Tiêu đề
        start: '2014-08-20T16:00:00',   =>Thời gian bắt đầu
        end: '2014-08-12T12:30:00',     =>Thời gian kết thúc
        url: 'http://google.com/'       =>Url nếu có thì khi click vào calendar thì sẽ được chuyển tới url này
    */
    public class CalendarViewModel
    {
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string url { get; set; }
        public string color { get; set; }
        public string bgcolor { get; set; }
    }
}
