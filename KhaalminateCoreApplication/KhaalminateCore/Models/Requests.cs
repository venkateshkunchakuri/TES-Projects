using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KhaalminateCore.Models
{
#pragma warning disable KApp
    public class PostInvoicesRequest
    {
        [Key]
        public int? Invid { get; set; }

        [Required]
        [StringLength(200)]
        public string Invno { get; set; }

        [Required]
        public string SearchDetails { get; set; }

        [Required]
        [StringLength(200)]
        public string POno { get; set; }
        public DateTime? PODate { get; set; }
        public DateTime? InvDate { get; set; }

        [Required]
        public decimal? InvAmt { get; set; }

        [Required]
        [StringLength(200)]
        public string FileNo { get; set; }

        [Required]
        [StringLength(200)]
        public string FromAddr { get; set; }

        [Required]
        [StringLength(200)]
        public string ToAddr { get; set; }

        [Required]
        [StringLength(200)]
        public string ShipAddr { get; set; }

        [Required]
        public decimal? InvAmtBT { get; set; }

        [Required]
        public decimal? CGST { get; set; }

        [Required]
        public decimal? SGST { get; set; }

        [Required]
        public decimal? TGST { get; set; }

        [Required]
        [StringLength(200)]
        public string AcctDet { get; set; }
        public string Declare { get; set; }
        public string Note { get; set; }

        [Required]
        [StringLength(200)]
        public string DCNO { get; set; }
        public string DCDate { get; set; }
        public DateTime? InsDate { get; set; }

        [Required]
        [StringLength(200)]
        public string InsAddr { get; set; }

        [Required]
        public int? BudEsti { get; set; }

        [Required]
        [StringLength(200)]
        public string QuotNo { get; set; }
    }

    public class PutInvoicesRequest
    {
        [Required]
        [StringLength(200)]
        public string Invno { get; set; }
    }

    public static class Extensions
    {
        public static Invoice ToEntity(this PostInvoicesRequest request)
            => new Invoice
            {
                Invno = request.Invno,
                InvAmt = request.InvAmt,
                InvAmtBT = request.InvAmtBT,
                CGST = request.CGST,
                SGST = request.SGST,
                TGST = request.TGST,
                POno = request.POno,
                AcctDet = request.AcctDet,
                Declare = request.Declare,
                Note = request.Note,
                FileNo = request.FileNo,
                FromAddr = request.FromAddr,
                ToAddr = request.ToAddr,
                ShipAddr = request.ShipAddr,
                DCNO = request.DCNO,
                QuotNo = request.QuotNo,
                InsAddr = request.InsAddr,
                BudEsti = request.BudEsti
            };
    }
#pragma warning restore KApp
}
