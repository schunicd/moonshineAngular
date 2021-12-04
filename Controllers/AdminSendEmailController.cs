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
using moonshineAngular.Models;

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

        // POST: api/AdminSendEmail/email
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("{email}")]
        public  void PostCustomer([FromRoute] Email email)
        {
            
            var customerEmails = _context.Customers.ToList();
            var image = Request.Form.Files[0];

            Console.WriteLine(email.subject);
            Console.WriteLine(email.body);
            Console.WriteLine(image.FileName);
            Console.WriteLine(image.OpenReadStream());
             
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
                    message.AlternateViews.Add(MailingList_Body(email, image));
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

        // POST: api/AdminSendEmail/Reservation
        [HttpPost]
        [Route("Reservation")]
        public  void PostReservation(ReservationEmail email)
        {
            
            var customerEmails = _context.Customers.ToList();

             
            string from = "schunicd@gmail.com"; //From address    
            string to = "";
            
            
            to = email.email; //To address   
            MailMessage message = new MailMessage(from, to);  
            message.Subject = email.subject;  
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.AlternateViews.Add(ReservationEmail_Body(email));
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

        private AlternateView MailingList_Body(Email email, IFormFile image){
            //string path = ("C:\Users\derek\Desktop\\band.jpg");
            //LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
            //Img.ContentId = "MyImage";
            string str = @"  
            <head>
            <title></title>
            <meta charset='utf-8'/>
            <meta content='width=device-width, initial-scale=1.0' name='viewport'/>
            <!--[if mso]><xml><o:OfficeDocumentSettings><o:PixelsPerInch>96</o:PixelsPerInch><o:AllowPNG/></o:OfficeDocumentSettings></xml><![endif]-->
            <!--[if !mso]><!-->
            <link href='https://fonts.googleapis.com/css?family=Merriweather' rel='stylesheet' type='text/css'/>
            <link href='https://fonts.googleapis.com/css?family=Droid+Serif' rel='stylesheet' type='text/css'/>
            <link href='https://fonts.googleapis.com/css?family=Bitter' rel='stylesheet' type='text/css'/>
            <!--<![endif]-->
            <style>
                    * {
                        box-sizing: border-box;
                    }

                    body {
                        margin: 0;
                        padding: 0;
                    }

                    a[x-apple-data-detectors] {
                        color: inherit !important;
                        text-decoration: inherit !important;
                    }

                    #MessageViewBody a {
                        color: inherit;
                        text-decoration: none;
                    }

                    p {
                        line-height: inherit
                    }

                    @media (max-width:700px) {
                        .icons-inner {
                            text-align: center;
                        }

                        .icons-inner td {
                            margin: 0 auto;
                        }

                        .row-content {
                            width: 100% !important;
                        }

                        .image_block img.big {
                            width: auto !important;
                        }

                        .stack .column {
                            width: 100%;
                            display: block;
                        }
                    }
                </style>
            </head>
            <body style='background-color: #5f6571; margin: 0; padding: 0; -webkit-text-size-adjust: none; text-size-adjust: none;'>
            <table border='0' cellpadding='0' cellspacing='0' class='nl-container' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #5f6571;' width='100%'>
            <tbody>
            <tr>
            <td>
            <table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-1' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
            <tbody>
            <tr>
            <td>
            <table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #952121; color: #000000; width: 680px;' width='680'>
            <tbody>
            <tr>
            <td class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 0px; padding-bottom: 0px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
            <table border='0' cellpadding='10' cellspacing='0' class='divider_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
            <tr>
            <td>
            <div align='center'>
            <table border='0' cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
            <tr>
            <td class='divider_inner' style='font-size: 1px; line-height: 1px; border-top: 1px solid #000000;'><span> </span></td>
            </tr>
            </table>
            </div>
            </td>
            </tr>
            </table>
            </td>
            </tr>
            </tbody>
            </table>
            </td>
            </tr>
            </tbody>
            </table>
            <table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-2' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
            <tbody>
            <tr>
            <td>
            <table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #26282c; color: #000000; width: 680px;' width='680'>
            <tbody>
            <tr>
            <td class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 0px; padding-bottom: 0px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
            <table border='0' cellpadding='0' cellspacing='0' class='image_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
            <tr>
            <td style='width:100%;padding-right:0px;padding-left:0px;padding-top:55px;'>
            <div align='center' style='line-height:10px'><img class='big' src='https://moonshinephotos.s3.amazonaws.com/ReceiptPhotos/navMenu_MoonshineLogo.jpg' style='display: block; height: auto; border: 0; width: 680px; max-width: 100%;' width='680'/></div>
            </td>
            </tr>
            </table>
            <table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
            <tr>
            <td style='padding-top:65px;padding-right:10px;padding-bottom:50px;padding-left:10px;'>
            <div style='font-family: sans-serif'>
            <div style='font-size: 14px; mso-line-height-alt: 16.8px; color: #555555; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
            <p style='margin: 0; font-size: 14px; text-align: justify;'><span style='color:#ffffff;'>" + email.body + @"</span></p>
            </div>
            </div>
            </td>
            </tr>
            </table>
            </td>
            </tr>
            </tbody>
            </table>
            </td>
            </tr>
            </tbody>
            </table>
            <table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-3' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
            <tbody>
            <tr>
            <td>
            <table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #26282c; color: #000000; width: 680px;' width='680'>
            <tbody>
            <tr>
            <td class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 0px; padding-bottom: 0px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
            <table border='0' cellpadding='0' cellspacing='0' class='image_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
            <tr>
            <td style='width:100%;padding-right:0px;padding-left:0px;padding-bottom:30px;'>
            <div align='center' style='line-height:10px'><img src='" + image.OpenReadStream() + @"' style='display: block; height: auto; border: 0; width: 550px; max-width: 100%;' width='550'/></div>
            </td>
            </tr>
            </table>
            </td>
            </tr>
            </tbody>
            </table>
            </td>
            </tr>
            </tbody>
            </table>
            <table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-4' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
            <tbody>
            <tr>
            <td>
            <table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #26282c; color: #000000; width: 680px;' width='680'>
            <tbody>
            <tr>
            <td class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 0px; padding-bottom: 0px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
            <table border='0' cellpadding='15' cellspacing='0' class='social_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
            <tr>
            <td>
            <table align='center' border='0' cellpadding='0' cellspacing='0' class='social-table' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='184px'>
            <tr>
            <td style='padding:0 7px 0 7px;'><a href='https://www.facebook.com/themoonshinecafe/' target='_blank'><img alt='Facebook' height='32' src='https://moonshinephotos.s3.amazonaws.com/ReceiptPhotos/facebook.png' style='display: block; height: auto; border: 0;' title='Facebook' width='32'/></a></td>
            <td style='padding:0 7px 0 7px;'><a href='https://twitter.com/moonshinecafe' target='_blank'><img alt='Twitter' height='32' src='https://moonshinephotos.s3.amazonaws.com/ReceiptPhotos/twitter.png' style='display: block; height: auto; border: 0;' title='Twitter' width='32'/></a></td>
            <td style='padding:0 7px 0 7px;'><a href='https://www.instagram.com/moonshinecafe_oakville/' target='_blank'><img alt='Instagram' height='32' src='https://moonshinephotos.s3.amazonaws.com/ReceiptPhotos/instagram.png' style='display: block; height: auto; border: 0;' title='Instagram' width='32'/></a></td>
            <td style='padding:0 7px 0 7px;'><a href='https://www.patreon.com/themoonshinecafe' target='_blank'><img alt='Patreon' height='32' src='https://moonshinephotos.s3.amazonaws.com/ReceiptPhotos/Patreon.png' style='display: block; height: auto; border: 0;' title='Patreon' width='32'/></a></td>
            </tr>
            </table>
            </td>
            </tr>
            </table>
            <table border='0' cellpadding='5' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
            <tr>
            <td>
            <div style='font-family: sans-serif'>
            <div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #393d47; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
            <p style='margin: 0; font-size: 14px; text-align: center;'><span style='color:#b6becf;'>© 2021 The Moonshine Café | All rights reserved.</span></p>
            </div>
            </div>
            </td>
            </tr>
            </table>
            <table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
            <tr>
            <td style='padding-bottom:65px;padding-left:5px;padding-right:5px;padding-top:5px;'>
            <div style='font-family: sans-serif'>
            <div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #393d47; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
            <p style='margin: 0; font-size: 14px; text-align: center;'><span style='color:#b6becf;'>137 KERR St. Oakville, ON, L6Z 3A6</span></p>
            </div>
            </div>
            </td>
            </tr>
            </table>
            </td>
            </tr>
            </tbody>
            </table>
            </td>
            </tr>
            </tbody>
            </table>
            <table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-5' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-position: top center;' width='100%'>
            <tbody>
            <tr>
            <td>
            <table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #952121; color: #000000; width: 680px;' width='680'>
            <tbody>
            <tr>
            <td class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
            <table border='0' cellpadding='10' cellspacing='0' class='divider_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
            <tr>
            <td>
            <div align='center'>
            <table border='0' cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
            <tr>
            <td class='divider_inner' style='font-size: 1px; line-height: 1px; border-top: 1px solid #000000;'><span> </span></td>
            </tr>
            </table>
            </div>
            </td>
            </tr>
            </table>
            </td>
            </tr>
            </tbody>
            </table>
            </td>
            </tr>
            </tbody>
            </table>
            <table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-6' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
            <tbody>
            <tr>
            <td>
            <table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; color: #000000; width: 680px;' width='680'>
            <tbody>
            <tr>
            <td class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
            <table border='0' cellpadding='0' cellspacing='0' class='icons_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
            <tr>
            <td style='color:#9d9d9d;font-family:inherit;font-size:15px;padding-bottom:5px;padding-top:5px;text-align:center;'>
            <table cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
            <tr>
            <td style='text-align:center;'>
            <!--[if vml]><table align='left' cellpadding='0' cellspacing='0' role='presentation' style='display:inline-block;padding-left:0px;padding-right:0px;mso-table-lspace: 0pt;mso-table-rspace: 0pt;'><![endif]-->
            <!--[if !vml]><!-->
            <table cellpadding='0' cellspacing='0' class='icons-inner' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; display: inline-block; margin-right: -4px; padding-left: 0px; padding-right: 0px;'>
            <!--<![endif]-->
            <tr>
            </tr>
            </table>
            </td>
            </tr>
            </table>
            </td>
            </tr>
            </table>
            </td>
            </tr>
            </tbody>
            </table>
            </td>
            </tr>
            </tbody>
            </table>
            </td>
            </tr>
            </tbody>
            </table><!-- End -->
            </body>  
            ";  
        AlternateView AV = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);  
        //AV.LinkedResources.Add(Img);  
        return AV;  
        }

        private AlternateView ReservationEmail_Body(ReservationEmail email){

            string str = @" 
            <head>
<title></title>
<meta charset='utf-8'/>
<meta content='width=device-width, initial-scale=1.0' name='viewport'/>
<!--[if mso]><xml><o:OfficeDocumentSettings><o:PixelsPerInch>96</o:PixelsPerInch><o:AllowPNG/></o:OfficeDocumentSettings></xml><![endif]-->
<!--[if !mso]><!-->
<link href='https://fonts.googleapis.com/css?family=Merriweather' rel='stylesheet' type='text/css'/>
<link href='https://fonts.googleapis.com/css?family=Droid+Serif' rel='stylesheet' type='text/css'/>
<link href='https://fonts.googleapis.com/css?family=Bitter' rel='stylesheet' type='text/css'/>
<!--<![endif]-->
<style>
		* {
			box-sizing: border-box;
		}

		body {
			margin: 0;
			padding: 0;
		}

		th.column {
			padding: 0
		}

		a[x-apple-data-detectors] {
			color: inherit !important;
			text-decoration: inherit !important;
		}

		#MessageViewBody a {
			color: inherit;
			text-decoration: none;
		}

		p {
			line-height: inherit
		}
		.fontStyling{
			font-size: 12px; 
			font-family: 'Merriwheater', 'Georgia', serif; 
			mso-line-height-alt: 14.399999999999999px; 
			color: #393d47; 
			line-height: 1.2;
		}
		@media (max-width:700px) {
			.icons-inner {
				text-align: center;
			}

			.icons-inner td {
				margin: 0 auto;
			}

			.row-content {
				width: 100% !important;
			}

			.image_block img.big {
				width: auto !important;
			}

			.stack .column {
				width: 100%;
				display: block;
			}
		}
	</style>
</head>
<body style='background-color: #5f6571; margin: 0; padding: 0; -webkit-text-size-adjust: none; text-size-adjust: none;'>
<table border='0' cellpadding='0' cellspacing='0' class='nl-container' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #5f6571;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-1' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #831c1c; color: #000000;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 0px; padding-bottom: 0px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
<table border='0' cellpadding='10' cellspacing='0' class='divider_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td>
<div align='center'>
<table border='0' cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td class='divider_inner' style='font-size: 1px; line-height: 1px; border-top: 1px solid #000000;'><span> </span></td>
</tr>
</table>
</div>
</td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-2' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #26282c; color: #000000;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 0px; padding-bottom: 0px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
<table border='0' cellpadding='0' cellspacing='0' class='image_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td style='width:100%;padding-right:0px;padding-left:0px;padding-top:55px;'>
<div align='center' style='line-height:10px'><img alt='Logo' class='big' src='https://moonshinephotos.s3.amazonaws.com/ReceiptPhotos/navMenu_MoonshineLogo.jpg' style='display: block; height: auto; border: 0; width: 680px; max-width: 100%;' title='Logo' width='680'/></div>
</td>
</tr>
</table>
<table border='0' cellpadding='0' cellspacing='0' class='image_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td style='width:100%;padding-right:0px;padding-left:0px;padding-top:55px;padding-bottom:40px;'>
<div align='center' style='line-height:10px'><img alt='Thank You. Here is your receipt.' class='big' src='https://moonshinephotos.s3.amazonaws.com/ReceiptPhotos/Thankyou2.jpg' style='display: block; height: auto; border: 0; width: 680px; max-width: 100%;' title='Thank You. Here is your receipt.' width='680'/></div>
</td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-3' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-position: top center;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #26282c; color: #333;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; background-color: #3e434d; border-left: 0px solid #26282C; border-top: 0px; border-right: 0px; border-bottom: 0px;' width='33.333333333333336%'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-top:35px;padding-right:10px;padding-bottom:15px;padding-left:10px;'>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='margin: 0; font-size: 12px; text-align: center;'><span style='color:#ddc403;font-size:18px;'>Event Name:</span></p>
</div>
</div>
</td>
</tr>
</table>
</th>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; background-color: #3e434d; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='66.66666666666667%'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-bottom:35px;padding-top:35px;'>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='color:#ffffff;margin: 0; font-size: 14px; text-align: center; mso-line-height-alt: 14.399999999999999px;'>" + email.eventName + @"</p>
</div>
</div>
</td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-4' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-position: top center;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #26282c; color: #333;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; background-color: #3e434d; border-left: 0px solid #26282C; border-top: 0px; border-right: 0px; border-bottom: 0px;' width='33.333333333333336%'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-top:35px;padding-right:10px;padding-bottom:15px;padding-left:10px;'>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='margin: 0; font-size: 12px; text-align: center;'><span style='color:#ddc403;font-size:18px;'>Event Date:</span></p>
</div>
</div>
</td>
</tr>
</table>
</th>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; background-color: #3e434d; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='66.66666666666667%'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-bottom:35px;padding-top:35px;'>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='color:#ffffff;margin: 0; font-size: 14px; text-align: center; mso-line-height-alt: 14.399999999999999px;'>" + email.eventDate + @"</p>
</div>
</div>
</td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-5' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-position: top center;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #26282c; color: #333;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; background-color: #3e434d; border-left: 0px solid #26282C; border-top: 0px; border-right: 0px; border-bottom: 0px;' width='33.333333333333336%'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-top:35px;padding-right:10px;padding-bottom:15px;padding-left:10px;'>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='margin: 0; font-size: 12px; text-align: center;'><span style='color:#ddc403;font-size:18px;'>Purchased By:</span></p>
</div>
</div>
</td>
</tr>
</table>
</th>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; background-color: #3e434d; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='66.66666666666667%'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-bottom:35px;padding-top:35px;'>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='color:#ffffff;margin: 0; font-size: 14px; text-align: center; mso-line-height-alt: 14.399999999999999px;'>" + email.name + @"</p>
</div>
</div>
</td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-6' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-position: top center;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #26282c; color: #333;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; background-color: #3e434d; border-left: 0px solid #26282C; border-top: 0px; border-right: 0px; border-bottom: 0px;' width='33.333333333333336%'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-top:35px;padding-right:10px;padding-bottom:15px;padding-left:10px;'>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='margin: 0; font-size: 12px; text-align: center;'><span style='color:#ddc403;font-size:18px;'>Date Purchased:</span></p>
</div>
</div>
</td>
</tr>
</table>
</th>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; background-color: #3e434d; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='66.66666666666667%'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-bottom:35px;padding-top:35px;'>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='color:#ffffff;margin: 0; font-size: 14px; text-align: center; mso-line-height-alt: 14.399999999999999px;'>" + email.purchaseDate + @"</p>
</div>
</div>
</td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-7' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-position: top center;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #26282c; color: #333;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; background-color: #3e434d; border-left: 0px solid #26282C; border-top: 0px; border-right: 0px; border-bottom: 0px;' width='33.333333333333336%'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-top:35px;padding-right:10px;padding-bottom:15px;padding-left:10px;'>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='margin: 0; font-size: 12px; text-align: center;'><span style='color:#ddc403;font-size:18px;'>Total Seats:</span></p>
</div>
</div>
</td>
</tr>
</table>
</th>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; background-color: #3e434d; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='66.66666666666667%'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-bottom:35px;padding-top:35px;'>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='color:#ffffff;margin: 0; font-size: 14px; text-align: center; mso-line-height-alt: 14.399999999999999px;'>" + email.totalSeats + @"</p>
</div>
</div>
</td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-7' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-position: top center;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #26282c; color: #333;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; background-color: #3e434d; border-left: 0px solid #26282C; border-top: 0px; border-right: 0px; border-bottom: 0px;' width='33.333333333333336%'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-top:35px;padding-right:10px;padding-bottom:15px;padding-left:10px;'>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='margin: 0; font-size: 12px; text-align: center;'><span style='color:#ddc403;font-size:18px;'>Cost Per Seat:</span></p>
</div>
</div>
</td>
</tr>
</table>
</th>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; background-color: #3e434d; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='66.66666666666667%'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-bottom:35px;padding-top:35px;'>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='color:#ffffff;margin: 0; font-size: 14px; text-align: center; mso-line-height-alt: 14.399999999999999px;'>$" + email.ticketPrice + @"</p>
</div>
</div>
</td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-8' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #26282c; color: #000000;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 0px; padding-bottom: 0px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
<div class='spacer_block' style='height:30px;line-height:30px;font-size:1px;'> </div>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-9' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #3e434d; color: #000000;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top;' width='50%'>
<table border='0' cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td style='width:30px;background-color:#26282C'> </td>
<td style='padding-left:25px;border-top:0px;border-right:0px;border-bottom:0px;width:310px;'>
<table border='0' cellpadding='15' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td>
<div style='font-family: serif'>
<div class='fontStyling'>
<p style='margin: 0; font-size: 14px; text-align: left;'><span style='color:#ffffff;'><strong><span style='font-size:16px;'>Subtotal</span></strong></span></p>
</div>
</div>
</td>
</tr>
</table>
</td>
</tr>
</table>
</th>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top;' width='50%'>
<table border='0' cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td style='padding-right:25px;border-top:0px;border-bottom:0px;border-left:0px;width:310px;'>
<table border='0' cellpadding='15' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #393d47; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p  style='color:#ffffff;margin: 0; font-size: 14px; text-align: right; mso-line-height-alt: 14.399999999999999px;'>$" + email.subtotal + @"</p>
</div>
</div>
</td>
</tr>
</table>
</td>
<td style='width:30px;background-color:#26282C'> </td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-10' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #3e434d; color: #000000;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top;' width='100%'>
<table border='0' cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td style='width:30px;background-color:#26282C'> </td>
<td style='padding-top:0px;padding-bottom:0px;border-top:0px;border-bottom:0px;width:620px;'>
<table border='0' cellpadding='5' cellspacing='0' class='divider_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td>
<div align='center'>
<table border='0' cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td class='divider_inner' style='font-size: 1px; line-height: 1px; border-top: 2px solid #5F6571;'><span> </span></td>
</tr>
</table>
</div>
</td>
</tr>
</table>
</td>
<td style='width:30px;background-color:#26282C'> </td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-11' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #3e434d; color: #000000;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top;' width='50%'>
<table border='0' cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td style='width:30px;background-color:#26282C'> </td>
<td style='padding-left:25px;border-top:0px;border-right:0px;border-bottom:0px;width:310px;'>
<table border='0' cellpadding='15' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td>
<div style='font-family: serif'>
<div class='fontStyling'>
<p style='margin: 0; font-size: 14px; text-align: left;'><span style='color:#ffffff;'><strong><span style='font-size:16px;'>Tax</span></strong></span></p>
</div>
</div>
</td>
</tr>
</table>
</td>
</tr>
</table>
</th>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top;' width='50%'>
<table border='0' cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td style='padding-right:25px;border-top:0px;border-bottom:0px;border-left:0px;width:310px;'>
<table border='0' cellpadding='15' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #393d47; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p  style='color:#ffffff;margin: 0; font-size: 14px; text-align: right; mso-line-height-alt: 14.399999999999999px;'>$" + email.taxes + @"</p>
</div>
</div>
</td>
</tr>
</table>
</td>
<td style='width:30px;background-color:#26282C'> </td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-12' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #3e434d; color: #000000;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top;' width='100%'>
<table border='0' cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td style='width:30px;background-color:#26282C'> </td>
<td style='padding-top:0px;padding-bottom:0px;border-top:0px;border-bottom:0px;width:620px;'>
<table border='0' cellpadding='5' cellspacing='0' class='divider_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td>
<div align='center'>
<table border='0' cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td class='divider_inner' style='font-size: 1px; line-height: 1px; border-top: 2px solid #5F6571;'><span> </span></td>
</tr>
</table>
</div>
</td>
</tr>
</table>
</td>
<td style='width:30px;background-color:#26282C'> </td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-13' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #5f6571; color: #000000;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top;' width='50%'>
<table border='0' cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td style='width:30px;background-color:#26282C'> </td>
<td style='padding-left:25px;border-top:0px;border-right:0px;border-bottom:0px;width:310px;'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-bottom:20px;padding-left:15px;padding-right:15px;padding-top:20px;'>
<div style='font-family: serif'>
<div class='fontStyling'>
<p style='margin: 0; font-size: 14px; text-align: left;'><span style='color:#ffffff;'><strong><span style='font-size:16px;'>Total</span></strong></span></p>
</div>
</div>
</td>
</tr>
</table>
</td>
</tr>
</table>
</th>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top;' width='50%'>
<table border='0' cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td style='padding-right:25px;border-top:0px;border-bottom:0px;border-left:0px;width:310px;'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-bottom:20px;padding-left:15px;padding-right:15px;padding-top:20px;'>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #393d47; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='color:#ffffff;margin: 0; font-size: 14px; text-align: right; mso-line-height-alt: 14.399999999999999px;'>$" + email.totalCost + @"</p>
</div>
</div>
</td>
</tr>
</table>
</td>
<td style='width:30px;background-color:#26282C'> </td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-14' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #26282c; color: #000000;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 0px; padding-bottom: 0px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
<table border='0' cellpadding='0' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td style='padding-bottom:15px;padding-left:30px;padding-right:30px;padding-top:25px;'>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #393d47; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='margin: 0; font-size: 14px; text-align: center;'><span style='color:#b6becf;'>Receipt # " + email.paypalID + @"</span></p>
</div>
</div>
</td>
</tr>
</table>
<table border='0' cellpadding='0' cellspacing='0' class='social_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td style='padding-bottom:15px;padding-left:15px;padding-right:15px;padding-top:55px;text-align:center;'>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='social-table' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='184.02501954652072px'>
<tr>
<td style='padding:0 7px 0 7px;'><a href='https://www.facebook.com/themoonshinecafe/' target='_blank'><img alt='Facebook' height='32' src='https://moonshinephotos.s3.amazonaws.com/ReceiptPhotos/facebook.png' style='display: block; height: auto; border: 0;' title='facebook' width='32'/></a></td>
<td style='padding:0 7px 0 7px;'><a href='https://twitter.com/moonshinecafe' target='_blank'><img alt='Twitter' height='32' src='https://moonshinephotos.s3.amazonaws.com/ReceiptPhotos/twitter.png' style='display: block; height: auto; border: 0;' title='twitter' width='32'/></a></td>
<td style='padding:0 7px 0 7px;'><a href='https://www.instagram.com/moonshinecafe_oakville/' target='_blank'><img alt='Instagram' height='32' src='https://moonshinephotos.s3.amazonaws.com/ReceiptPhotos/instagram.png' style='display: block; height: auto; border: 0;' title='instagram' width='32'/></a></td>
<td style='padding:0 7px 0 7px;'><a href='https://www.patreon.com/themoonshinecafe' target='_blank'><img alt='Custom' height='32' src='https://moonshinephotos.s3.amazonaws.com/ReceiptPhotos/Patreon.png' style='display: block; height: auto; border: 0;' title='Custom' width='32.02501954652072'/></a></td>
</tr>
</table>
</td>
</tr>
</table>
<table border='0' cellpadding='5' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #393d47; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='margin: 0; font-size: 14px; text-align: center;'><span style='color:#b6becf;'>© 2021 The Moonshine Café | All rights reserved.</span></p>
</div>
</div>
</td>
</tr>
</table>
<table border='0' cellpadding='5' cellspacing='0' class='text_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;' width='100%'>
<tr>
<td>
<div style='font-family: sans-serif'>
<div style='font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #393d47; line-height: 1.2; font-family: Merriwheater, Georgia, serif;'>
<p style='margin: 0; font-size: 14px; text-align: center;'><span style='color:#b6becf;'>137 KERR St. Oakville, ON, L6Z 3A6</span></p>
</div>
</div>
</td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-15' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-position: top center;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #831c1c; color: #000000;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
<table border='0' cellpadding='10' cellspacing='0' class='divider_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td>
<div align='center'>
<table border='0' cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td class='divider_inner' style='font-size: 1px; line-height: 1px; border-top: 1px solid #000000;'><span> </span></td>
</tr>
</table>
</div>
</td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row row-16' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tbody>
<tr>
<td>
<table align='center' border='0' cellpadding='0' cellspacing='0' class='row-content stack' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; color: #000000;' width='680'>
<tbody>
<tr>
<th class='column' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;' width='100%'>
<table border='0' cellpadding='0' cellspacing='0' class='icons_block' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td style='color:#9d9d9d;font-family:inherit;font-size:15px;padding-bottom:5px;padding-top:5px;text-align:center;'>
<table cellpadding='0' cellspacing='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt;' width='100%'>
<tr>
<td style='text-align:center;'>
<!--[if vml]><table align='left' cellpadding='0' cellspacing='0' role='presentation' style='display:inline-block;padding-left:0px;padding-right:0px;mso-table-lspace: 0pt;mso-table-rspace: 0pt;'><![endif]-->
<!--[if !vml]><!-->
<table cellpadding='0' cellspacing='0' class='icons-inner' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; display: inline-block; margin-right: -4px; padding-left: 0px; padding-right: 0px;'>
<!--<![endif]-->
</table>
</td>
</tr>
</table>
</td>
</tr>
</table>
</th>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table><!-- End -->
</body>
            ";

        AlternateView AV = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
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
