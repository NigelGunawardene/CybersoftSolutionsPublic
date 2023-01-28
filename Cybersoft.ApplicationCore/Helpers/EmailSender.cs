using Cybersoft.ApplicationCore.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using Cybersoft.ApplicationCore.Models;

namespace Cybersoft.ApplicationCore.Helpers
{

    public class EmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async void sendOrderEmail(OrderModel order, List<OrderDetails> currentOrderDetails)
        {
            try
            {
                MailjetClient client = new MailjetClient(_configuration.GetValue<string>("EmailSender:MJ_APIKEY_PUBLIC"), _configuration.GetValue<string>("EmailSender:MJ_APIKEY_PRIVATE"));
                MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource,
                }
                .Property(Send.FromEmail, "<KeyVault>")
                .Property(Send.FromName, "<KeyVault>")
                .Property(Send.Subject, "Order confirmation for order: " + order.PublicOrderNumber)
                .Property(Send.TextPart, generateEmailBody(order, currentOrderDetails))
                .Property(Send.HtmlPart, generateEmailBodyInHtml(order, currentOrderDetails))
                .Property(Send.Recipients, new JArray {
                 new JObject {
                 {"Email", order.User.Email}
                 }
                 });
                MailjetResponse response = await client.PostAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                    Console.WriteLine(response.GetData());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        string generateEmailBody(OrderModel order, List<OrderDetails> currentOrderDetails)
        {
            string body =
                "Hi " + order.User.FirstName + ",\n \n" +
                "This is your order confirmation for: " + order.PublicOrderNumber + "\n";
            foreach (var detail in currentOrderDetails)
            {
                body = body + detail.Product.Name + " : " + detail.Product.Price + "LKR x " + detail.Quantity.ToString() + " = " + detail.TotalPrice + "LKR \n";
            }

            return body;
        }

        string generateEmailBodyInHtml(OrderModel order, List<OrderDetails> currentOrderDetails)
        {
            string body =
                "<strong>Hi " + order.User.FirstName + ",</strong><br><br>" +
                "<strong>This is your order confirmation for: " + order.PublicOrderNumber + "-<strong><br><br>";
            foreach (var detail in currentOrderDetails)
            {
                body = body + detail.Product.Name + " : " + detail.Product.Price + "LKR x " + detail.Quantity.ToString() + " = " + detail.TotalPrice + "LKR <br>";
            }


            return body;
        }
    }
}
