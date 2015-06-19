using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Umbraco_Identity_Playground.Auth
{
    [TableName("umbracoUserTwoFactor")]
    [PrimaryKey("Id", autoIncrement = true)]
    [ExplicitColumns]
    public class TwoFactorAuthPoco
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