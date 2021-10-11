using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheMoonshineCafe.Data;
using TheMoonshineCafe.Models;
using System.Net;
using System.Net.Mail; 
using System.Text;
 
using System.IO;
using System.Net.Mime;  

namespace TheMoonshineCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminSendEmailController : ControllerBase
    {
        private readonly MoonshineCafeContext _context;

        public AdminSendEmailController(MoonshineCafeContext context)
        {
            _context = context;
        }

        // GET: api/AdminSendEmail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/AdminSendEmail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/AdminSendEmail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AdminSendEmail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public  void PostCustomer(Email email)
        {
            
            var customerEmails = _context.Customers.ToList();

             
            string from = "schunicd@gmail.com"; //From address    
            string to = "";
            
            foreach (var e in customerEmails)
            {
                if(e.onMailingList){
                    to = e.email; //To address   
                    MailMessage message = new MailMessage(from, to);  
                    message.Subject = email.subject;  
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.AlternateViews.Add(Mail_Body(email));
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                    System.Net.NetworkCredential basicCredential1 = new  
                    System.Net.NetworkCredential("schunicd@gmail.com", "B00tleggers");  
                    client.EnableSsl = true;  
                    client.UseDefaultCredentials = false;
                    client.Credentials = basicCredential1;
                    try   
                    {  
                        client.Send(message);
                    }   
                    
                    catch (Exception ex)   
                    {  
                        throw ex;  
                    }  

                }
            }
            
        }

        private AlternateView Mail_Body(Email email){
            //string path = ("C:\Users\derek\Desktop\\band.jpg");
            //LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
            //Img.ContentId = "MyImage";
            string str = @"  
            <table>  
                <tr>  
                    <td> '" + email.body + @"'  
                    </td>  
                </tr>  
                <tr>  
                    <td>  
                      <img src=cid:MyImage  id='img' alt='' width='100px' height='100px'/>   
                    </td>  
                </tr></table>  
            ";  
        AlternateView AV = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);  
        //AV.LinkedResources.Add(Img);  
        return AV;  
        }

        /*
        // POST: api/AdminSendEmail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.id }, customer);
        }
        */

        // DELETE: api/AdminSendEmail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.id == id);
        }
    }
}
