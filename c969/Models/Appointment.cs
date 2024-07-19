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
