using System;
using System.Collections.Generic;
using System.Text;
using Castle.Windsor;

namespace KTPO4311.Husnutdinov.Lib.src.Common
{
    public static class CastleFactory
    {
        public static IWindsorContainer container { get; private set; }

        static CastleFactory()
        {
            container = new WindsorContainer();
        }
    }
}
