using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.Framework.Container
{
    public interface IDIPContainer
    {
        public void Register<TFrom, TTo>(string shortname = null, object[] paramlist = null,LifeTimeType lifeTimeType=LifeTimeType.Transient) where TTo : TFrom;

        public TFrom Resolve<TFrom>(string shortname = null, object[] paramlist = null);

        public IDIPContainer CreateSubDIPContainer();
    }
}
