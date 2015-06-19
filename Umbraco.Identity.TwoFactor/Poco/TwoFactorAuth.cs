using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Umbraco.Identity.TwoFactor.Poco
{
    
    [TableName("umbracoUserTwoFactor")]
    [PrimaryKey("Id", autoIncrement = true)]
    [ExplicitColumns]
    public class TwoFactorAuth
    {
        [Column("id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("userId")]
        public int UserId { get; set; }

        [Column("twoFactorEnabled")]
        public bool TwoFactorEnabled { get; set; }
    }
    
}
