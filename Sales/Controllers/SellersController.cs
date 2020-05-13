﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sales.Models;
using Sales.Models.ViewModels;

using Sales.Services;

namespace Sales.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {

            _sellerService = sellerService;
            _departmentService = departmentService;
        }

       

         
        
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }


        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var ViewModel = new SellerFormViewModel { Departments = departments };
            return View(ViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));

        }
        //esse metodo Delete não deleta, vai pra tela de confirmação do Delete.
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }



    }
}