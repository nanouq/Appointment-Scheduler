using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c969
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public string Start {  get; set; }
        public string End { get; set; }
        public string CreateDate {  get; set; }
        public string CreatedBy {  get; set; }
        public string LastUpdate {  get; set; }
        public string LastUpdateBy { get; set;}
        public string CustomerName {  get; set; }
        public string Username {  get; set; }
        public DateTime dtStart {  get; set; }
        public DateTime dtEnd {  get; set; }


        public Appointment()
        {

        }
        public Appointment(string type, DateTime start)
        {
            Type = type;
            dtStart = start;
        }

        public Appointment(string customerName, string type, DateTime start)
        {
            CustomerName = customerName; 
            Type = type;
            dtStart = start;
        }

        public Appointment(int appointmentId, string type, string customerName, DateTime start, DateTime end, string username)
        {
            AppointmentId = appointmentId;
            Type = type;
            CustomerName = customerName;
            dtStart = start;
            dtEnd = end;
            Username = username;
            
        }

        public Appointment(int appointmentID, int customerID, int userID, string type, string start, string end, string createDate, string createdBy, string lastUpdate, string lastUpdateBy) 
        { 
            AppointmentId = appointmentID;
            CustomerId = customerID;
            UserId = userID;
            Type = type;
            Start = start;
            End = end;
            CreateDate = createDate;
            CreatedBy = createdBy;
            LastUpdate = lastUpdate;
            LastUpdateBy = lastUpdateBy;
        }

    }
}
