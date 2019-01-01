namespace TaxiChain.Transactions
{
    using System.Collections.Generic;

    using NBlockchain.Models;

    /// <summary>
    /// Accept Reqest Instruction
    /// </summary>
    public class AcceptReqestInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        /// <value>
        /// The customer.
        /// </value>
        public string CustomerAddress { get; set; }

        /// <summary>
        /// Extracts the signable elements.
        /// </summary>
        /// <returns></returns>
        public override ICollection<byte[]> ExtractSignableElements()
        {
            return new List<byte[]>() { System.Text.Encoding.Unicode.GetBytes(this.CustomerAddress)};
        }
    }
}
