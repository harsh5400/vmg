using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameWork
{
   public class BusinessValidationResult
    {
        private bool status;
        private string result;

        public bool Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public string Result
        {
            get
            {
                return this.result;
            }
            set
            {
                this.result = value;
            }
        }

        public BusinessValidationResult()
        {
            this.status = true;
            this.result = string.Empty;
        }
    }

   public enum EntityState
   {
       NotTracked,
       Original,
       New,
       Modified,
       Detached,
       Deleted,
       CancelNew,
   }
}
