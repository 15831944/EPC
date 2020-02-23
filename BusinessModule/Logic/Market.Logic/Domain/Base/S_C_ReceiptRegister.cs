using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Market.Logic.Domain
{
    public partial class S_C_ReceiptRegister
    {
        public void Delete()
        {
            var dbContext = this.GetDbContext<MarketEntities>();
            if (dbContext.S_C_Receipt.Count(d => d.RegisterID == this.ID) > 0)
                throw new Formula.Exceptions.BusinessException("已经有到款认领的记录不能删除");
            dbContext.S_C_ReceiptRegister.Remove(this);
        }

        public void SumConfirmValue()
        {
            var dbContext = this.GetDbContext<MarketEntities>();
            if (dbContext.S_C_Receipt.Count(d => d.RegisterID == this.ID) > 0)
            {
                this.ConfirmValue = dbContext.S_C_Receipt.Where(d => d.RegisterID == this.ID).Sum(d => d.Amount);
            }
            else
            {
                this.ConfirmValue = 0;
            }
        }
    }
}
