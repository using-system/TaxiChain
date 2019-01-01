namespace TaxiChain.Transactions
{
    using System.Collections.Generic;

    using NBlockchain.Models;

    /// <summary>
    /// Accept Reqest Instruction
    /// </summary>
    [InstructionType("acceptrequest-v1")]
    public class AcceptReqestInstruction : Instruction
    {

        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        /// <value>
        /// The request identifier.
        /// </value>
        public byte[] RequestID { get; set; }

        /// <summary>
        /// Extracts the signable elements.
        /// </summary>
        /// <returns></returns>
        public override ICollection<byte[]> ExtractSignableElements()
        {
            return new List<byte[]>() { this.RequestID };
        }
    }
}
