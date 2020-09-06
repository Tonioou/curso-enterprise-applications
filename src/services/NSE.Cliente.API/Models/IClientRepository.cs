﻿using NSE.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Cliente.API.Models
{
    public interface IClientRepository : IRepository<Cliente>
    {
        void Adicionar(Cliente cliente);
        Task<IEnumerable<Cliente>> ObterTodos();
        Task<Cliente> ObterPorCpf(string cpf);
    }
}
