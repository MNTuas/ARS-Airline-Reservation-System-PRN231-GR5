using BusinessObjects.ResponseModels.Booking;
using BusinessObjects.ResponseModels.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Ultis
{
    public static class EmailTemplate
    {
        public static string CreatePasswordResetEmail(string fullname, string email, string newPassword)
        {
            var html = $@"<div style='font-family: Arial, sans-serif; color: #333;'>
             <p>Dear {fullname},</p>
             <hr>
             <p>
                You have requested to reset your password on {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} +07<br/>
                This is your new login information:<br/>
                    Email:    <strong>{email}</strong><br/>
                    New Password: <strong>{newPassword}</strong><br/>
                Please change your password after logging in.<br/>
            </p>
            <p>This is a computer-generated email. Please do not reply to this email.</p>
            <p>Best Regards<br/>
        </div>";

            return html;
        }

        public static string ListTicket(string fullname, string email, List<TicketResponseModel> tickets)
        {
            var ticketListHtml = new StringBuilder();
            ticketListHtml.Append("<ul>");

            foreach (var ticket in tickets)
            {
                ticketListHtml.Append($@"<li>
            Ticket ID: {ticket.Id}<br/>
            Class: {ticket.ClassName}<br/>
            Price: {ticket.ClassPrice}<br/>
            Passenger Name: {ticket.FirstName} {ticket.LastName}<br/>
            Gender: {ticket.Gender}<br/>
            Date of Birth: {ticket.Dob:yyyy-MM-dd}<br/>
            Country: {ticket.Country}<br/>
            Status: {ticket.Status}
        </li><br/>");
            }

            ticketListHtml.Append("</ul>");

            var html = $@"<div style='font-family: Arial, sans-serif; color: #333;'>
        <p>Dear {fullname},</p>
        <hr>
        <p>Your booking information:</p>
        <p>Email: <strong>{email}</strong><br/>
           Please find your tickets below:
        </p>
        {ticketListHtml}
        <p>This is a computer-generated email. Please do not reply to this email.</p>
        <p>Best Regards,<br/></p>
    </div>";

            return html.ToString();
        }



    }
}
