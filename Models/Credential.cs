using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfHostedPasswordManager.Models

{
    public class Credential
    { 
        /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //autoinkrementacja Id
        [Key, Column(Order = 0)] */                              //Id od 0
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string Website { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Notes { get; set; }

        //public virtual ApplicationUser ApplicationUser { get; set; }
        public Credential()
        {
            this.Id = Guid.NewGuid().ToString();   
        }
    }
    
}
