using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class GrupoUsuario

    [Table(Name = "PER_GrupoUsuario", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class GrupoUsuario : CType
    {
        #region Construtores
        public GrupoUsuario()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public GrupoUsuario(int? idGrupoUsuario)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDGrupoUsuario = idGrupoUsuario;
        }

        #endregion

        #region Destrutor
        ~GrupoUsuario() { Dispose(); }
        #endregion

        #region Attributes
        private Usuario usuario;
        private Grupo grupo;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDGrupoUsuario { get; set; }

        [Validate]
        [Property(IsField = true, Name = "idCredencial")]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDUsuario { get; set; }

        public Usuario Usuario
        {
            get
            {
                if (this.usuario == null)
                {
                    this.usuario = new Usuario();
                    this.usuario.IDUsuario = this.IDUsuario;
                }

                if (!this.usuario.IsFull)
                {
                    this.usuario.Transaction = this.Transaction;
                    this.usuario.Get();
                }

                return this.usuario;
            }
            set
            {
                this.usuario = value;
                if (value != null) this.IDUsuario = value.IDUsuario;
            }
        }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDGrupo { get; set; }

        public Grupo Grupo
        {
            get
            {
                if (this.grupo == null)
                {
                    this.grupo = new Grupo();
                    this.grupo.IDGrupo = this.IDGrupo;
                }

                if (!this.grupo.IsFull)
                {
                    this.grupo.Transaction = this.Transaction;
                    this.grupo.Get();
                }

                return this.grupo;
            }
            set
            {
                this.grupo = value;
                if (value != null) this.IDGrupo = value.IDGrupo;
            }
        }

        #endregion
        
        #region Methods

        public List<Usuario> GetUsuarios()
        {
            var usuarios = new List<Usuario>();
            var igu = this.Find();
            igu.ForEach(i => usuarios.Add( ((GrupoUsuario)i).Usuario ));
            return usuarios;
        }

        #endregion
    }
    #endregion
}
