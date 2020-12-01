using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebCarWash.Domain.Entities;

namespace WebCarWash.Model
{

    public class IndexViewModel
    {
        //https://progi.pro/kak-vernut-vibrannie-znacheniya-iz-dvuh-listboxes-v-kontroller-mvc-s-pomoshyu-britvi-i-s-9454056
        public IEnumerable<SelectListItem> Servises { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }

        [Required]
        public int? SelectedClientId { get; set; }
        public List<string> SelectedListServices { get; set; }

        public IndexViewModel()
        {
            Servises = new List<SelectListItem>();
            Clients = new List<SelectListItem>();
            SelectedListServices = new List<string>();

        }
    }

    public class OrderViewModel
    {
        //https://progi.pro/kak-vernut-vibrannie-znacheniya-iz-dvuh-listboxes-v-kontroller-mvc-s-pomoshyu-britvi-i-s-9454056
        public IEnumerable<SelectListItem> Servises { get; set; }

        public List<string> SelectedListServices { get; set; }

        [Required]
        public int ClientId { get; set; }

        public int OrderId { get; set; }

        public string ClientName { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderState State { get; set; }
        public decimal Price { get; set; }

        public int Amount { get; set; }

        public OrderViewModel()
        {
            Servises = new List<SelectListItem>();
            SelectedListServices = new List<string>();

        }
    }

}