using honzanoll.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace honzanoll.Auth.Models
{
    public class UserEntity : ModelBase
    {
        #region Properties

        [StringLength(255)]
        public string Email { get; set; }

        public string Password { get; set; }

        #endregion
    }
}
