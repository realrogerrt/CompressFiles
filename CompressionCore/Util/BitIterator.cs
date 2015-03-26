using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressionCore.Util
{
    public class BitIterator : IEnumerable<bool>
    {

        private byte branch;

        public BitIterator(byte branch)
        {
            this.branch = branch;
        }

        public IEnumerator<bool> GetEnumerator()
        {
            return GetReverseEnumerator();
            //for (int i = 0; i < 8; i++)
            //{
            //    yield return branch % 2 == 1;
            //    branch >>= 1;
            //}
        }
        private IEnumerator<bool> GetReverseEnumerator()
        {
            for (int i = 7; i >= 0; i--)
            {
                bool result = (branch >> i) % 2 == 1;
                yield return result;
            }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
