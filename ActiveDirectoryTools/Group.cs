﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryTools
{
    class Group : IGroup
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<UserAccount> GroupMembers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}