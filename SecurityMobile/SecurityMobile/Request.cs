using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityMobile
{
    public class Request
    {
        public int VisitRequestID { get; set; }
        public string UserName { get; set; }
        public string UserSunrame { get; set; }
        public string UserPatronymic { get; set; }
        public string VisitType { get; set; }
        public string Subdivision { get; set; }
        public string EmployeeSurname { get; set; }
        public DateTime VisitDateTime { get; set; }
    }
}
