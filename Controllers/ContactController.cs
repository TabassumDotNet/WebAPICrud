using ContactAPI.Data;
using ContactAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly ContactAPIdbContext dbcontext;

        public ContactController(ContactAPIdbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbcontext.Contacts.ToListAsync());
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContacts([FromRoute] Guid id)
        {
            var contact = await dbcontext.Contacts.FindAsync(id);

            if (contact != null)
            {
                return NotFound();
            }

            return Ok(contact);


        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequests addContactRequest)
        {

            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                PhonNumber = addContactRequest.PhonNumber
            };

            await dbcontext.Contacts.AddAsync(contact);
            await dbcontext.SaveChangesAsync();


            return Ok(contact);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequests UpdateContactRequest)

        {
            var contact = await dbcontext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.FullName = UpdateContactRequest.FullName;
                contact.Email = UpdateContactRequest.Email;
                contact.Address = UpdateContactRequest.Address;
                contact.PhonNumber = UpdateContactRequest.PhonNumber;

                await dbcontext.SaveChangesAsync();


                return Ok(contact);

            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id, DeleteContactRequests DeleteContactRequest)

        {

            var contact = await dbcontext.Contacts.FindAsync(id);
            if (contact != null)
            {

                dbcontext.Remove(contact);
                await dbcontext.SaveChangesAsync();
                return Ok(contact);

            }

            return NotFound();

        }
    }


}