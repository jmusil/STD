// <auto-generated />
namespace STD.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    public sealed partial class UserNames : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(UserNames));
        
        string IMigrationMetadata.Id
        {
            get { return "201312200005110_UserNames"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}