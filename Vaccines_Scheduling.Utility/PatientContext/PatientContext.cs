﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccines_Scheduling.Utility.PatientContext
{
    public class PatientContext : IPatientContext
    {
        public PatientContext() { }

        /// <summary>
        /// Horário em que o contexto foi aberto.
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Informações da requisição como IP e HTTP HEADERS.
        /// </summary>
        public ISourceInfo SourceInfo { get; set; }

        /// <summary>
        /// GUID para identificar unicamente uma requisição.
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Dados adicionais que podem ser armazenados no Contexto.
        /// </summary>
        public Hashtable AdditionalData { get; set; }

        /// <summary>
        /// Coleção de exceções não tratadas pelo desenvolvedor. A chave é um valor único para cada instância de <see cref="Exception"/>.
        /// </summary>
        public Hashtable UnhandledExceptions { get; set; } = new Hashtable();

        /// <summary>
        /// Estado do usuário no contexto
        /// </summary>
        public string Status { get; set; }
    }
}

