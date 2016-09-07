﻿
using Kinetix.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kinetix.Workflow.model
{
    /// <summary>
    /// This class defines the Workflow Definition.
    /// </summary>
    /// 
    [Table("WF_WORKFLOW_DEFINITION")]
    public partial class WfWorkflowDefinition
    {
        /// <summary>
        /// Constructeur.
        /// </summary>
        public WfWorkflowDefinition()
        {
            this.OnCreated();
        }

        /// <summary>
        /// Constructeur par recopie.
        /// </summary>
        /// <param name="bean">Source.</param>
        public WfWorkflowDefinition(WfWorkflowDefinition bean)
        {
            if (bean == null)
            {
                throw new ArgumentNullException(nameof(bean));
            }

            this.WfwdId = bean.WfwdId;
            this.Name = bean.Name;
            this.CreationDate = bean.CreationDate;
            this.WfadId = bean.WfadId;

            this.OnCreated(bean);
        }

        #region Meta données

        /// <summary>
        /// Type énuméré présentant les noms des colonnes en base.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "A corriger")]
        public enum Cols
        {
            /// <summary>
            /// Nom de la colonne en base associée à la propriété .
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Correspondance schéma persistence")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "Correspondance schéma persistence")]
            WFWD_ID,

            /// <summary>
            /// Nom de la colonne en base associée à la propriété .
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Correspondance schéma persistence")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "Correspondance schéma persistence")]
            NAME,

            /// <summary>
            /// Nom de la colonne en base associée à la propriété .
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Correspondance schéma persistence")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "Correspondance schéma persistence")]
            CREATION_DATE,

            /// <summary>
            /// Nom de la colonne en base associée à la propriété .
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Correspondance schéma persistence")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "Correspondance schéma persistence")]
            WFAD_ID,
        }

        #endregion


        [Column("WFWD_ID")]
        [Domain("DO_X_WORKFLOW_ID")]
        [Key]
        public long? WfwdId
        {
            get;
            set;
        }

        [Column("NAME")]
        [Domain("DO_X_WORKFLOW_LABEL")]
        public string Name
        {
            get;
            set;
        }

        [Column("CREATION_DATE")]
        [Domain("DO_X_WORKFLOW_DATE")]
        public string CreationDate
        {
            get;
            set;
        }

        [Column("WFADID")]
        [Domain("DO_X_WORKFLOW_ID")]
        public long? WfadId
        {
            get;
            set;
        }


        /// <summary>
        /// Methode d'extensibilité possible pour les constructeurs.
        /// </summary>
        partial void OnCreated();

        /// <summary>
        /// Methode d'extensibilité possible pour les constructeurs par recopie.
        /// </summary>
        /// <param name="bean">Source.</param>
        partial void OnCreated(WfWorkflowDefinition bean);

    }
}
