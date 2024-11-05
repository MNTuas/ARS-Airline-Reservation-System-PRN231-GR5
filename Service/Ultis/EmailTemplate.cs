using BusinessObjects.ResponseModels.Booking;
using BusinessObjects.ResponseModels.Flight;
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

        public static string ListTicket(string fullname, string email, List<TicketResponseModel> tickets, FlightResponseModel flightResponse)
        {
            var ticketListHtml = new StringBuilder();
            ticketListHtml.Append("<div style='font-family: Arial, sans-serif; color: #333;'>");

            // Thêm thông tin người dùng
            ticketListHtml.Append($@"<p>Dear {fullname},</p>
<p>Email: <strong>{email}</strong><br/>
   <p>Your booking information:</p>
      Please check your tickets below:
        </p>
   <hr>");

            foreach (var ticket in tickets)
            {
                // Giả sử mỗi vé có một URL khác nhau cho mã QR
                string qrCodeUrl = $"https://res.cloudinary.com/dtihkfbuk/image/upload/v1730701553/qrcode_132816701_28b47b1dadaec68ec9d21029dea48f0f.png"; // Đường dẫn tới mã QR của vé

                ticketListHtml.Append($@"
            <div style='border: 1px solid #ccc; border-radius: 5px; padding: 10px; margin-bottom: 10px; display: flex;'>
                <div style='flex: 1; padding-right: 10px;'>
                    <div style='border: 1px solid #000; padding: 5px; display: flex; justify-content: center; align-items: center;'>
                        <img src='{qrCodeUrl}' alt='QR Code' style='max-width: 100px; max-height: 100px;'/>
                    </div>
                </div>
                <div style='flex: 1; padding-right: 10px; border-right: 1px solid #ccc;'>
                    <strong>Passenger Name:</strong> {ticket.FirstName} {ticket.LastName}<br/>
                    <strong>Class:</strong> {ticket.ClassName}<br/>
                    <strong>Price:</strong> {String.Format("{0:#,##0} VND", Convert.ToDecimal(ticket.ClassPrice))}<br/>
                    <strong>Gender:</strong> {ticket.Gender}<br/>
                    <strong>Date of Birth:</strong> {ticket.Dob:yyyy-MM-dd}<br/>
                    <strong>Country:</strong> {ticket.Country}<br/>
                </div>
                <div style='flex: 1; padding-left: 10px;'>
                    <strong>Flight Number:</strong> {flightResponse.FlightNumber}<br/>
                    <strong>Airline:</strong> {flightResponse.Airlines}<br/>
                    <strong>Airplane Code:</strong> {flightResponse.AirplaneCode}<br/>
                    <strong>Departure Time:</strong> {flightResponse.DepartureTime:yyyy-MM-dd HH:mm}<br/>
                    <strong>Arrival Time:</strong> {flightResponse.ArrivalTime:yyyy-MM-dd HH:mm}<br/>
                    <strong>From:</strong> {flightResponse.FromName}<br/>
                    <strong>To:</strong> {flightResponse.ToName}<br/>
                </div>
            </div>");
            }

            ticketListHtml.Append("</div>");
            ticketListHtml.Append("<p>This is a computer-generated email. Please do not reply to this email.</p>");
            ticketListHtml.Append("<p>Best Regards,<br/></p>");

            return ticketListHtml.ToString();
        }




    }
}
