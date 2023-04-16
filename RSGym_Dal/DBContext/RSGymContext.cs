using RSGym_Dal.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace RSGym_Dal.DBContext
{
  
    public class RSGymContext : DbContext
    {
        #region Construtor (Connectionstring do app.config)

        public RSGymContext()
            : base("name=RSGymContext")
        {
        }
        #endregion

        #region Comportamento da criação da base dados
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // desativar a plurizacao das tabelas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // como a aplicação não tem deletes desativei o cascade effect da db
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
        #endregion

        #region Criação das tabelas
        public DbSet<User> User { get; set; }
        public DbSet<PostalCode> PostalCode { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<PersonalTrainer> PersonalTrainer { get; set; }
        public DbSet<Request> Request { get; set; }

        #endregion

    }



}