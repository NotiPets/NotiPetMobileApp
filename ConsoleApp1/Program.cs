using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using AutoMapper;
using DynamicData;
using ImTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotiPet.Data;
using NotiPet.Data.Dtos;
using NotiPet.Data.Mappers;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToUniversalTime());
            Console.WriteLine(DateTime.Now);
           var message =  @"<div class=""elementor-element elementor-element-bd275b8 elementor-widget-divider--view-line_text elementor-widget-divider--element-align-center elementor-widget elementor-widget-divider"" data-id=""bd275b8"" data-element_type=""widget"" data-widget_type=""divider.default"">
                            <div class=""elementor-widget-container"">
                            <h2 style=""text-align: center;""><strong>Sobre Nosotros</strong></h2>
                            <div class=""elementor-divider"" style=""text-align: center;""></div>
                            </div>
                            </div>
                            <div class=""elementor-element elementor-element-c1ce774 elementor-widget elementor-widget-text-editor"" data-id=""c1ce774"" data-element_type=""widget"" data-widget_type=""text-editor.default"">
                            <div class=""elementor-widget-container"" style=""text-align: justify;"">Groovet es un centro dedicado a ofrecer bienestar a tu mascota. Contamos con espacio para sus consultas de rutina, un &aacute;rea especializada de ba&ntilde;o y peluquer&iacute;a y una tienda con los mejores art&iacute;culos del mercado para las mascotas, tales como: alimentos, accesorios, art&iacute;culos de limpieza y medicamentos.</div>
                            <div class=""elementor-widget-container"" style=""text-align: justify;"">
                            <h4 class=""elementor-icon-box-title""><span></span></h4>
                            <h4 class=""elementor-icon-box-title""><span>Misi&oacute;n</span></h4>
                            <p class=""elementor-icon-box-description""></p>
                            <p class=""elementor-icon-box-description"">Entregar a las familias de mascotas un servicio integral y especializado dedicado a preservar la salud de las mismas bajo el esquema de citas peri&oacute;dicas tanto en el &aacute;rea m&eacute;dica como de est&eacute;tica.</p>
                            <h4 class=""elementor-icon-box-title""><span>Valores</span></h4>
                            <ul class=""elementor-icon-list-items elementor-inline-items"">
                            <li class=""elementor-icon-list-item elementor-inline-item""><span class=""elementor-icon-list-text"">Dedicaci&oacute;n</span></li>
                            <li class=""elementor-icon-list-item elementor-inline-item""><span class=""elementor-icon-list-icon""><i aria-hidden=""true"" class=""fas fa-paw""></i></span><span class=""elementor-icon-list-text"">Aceptaci&oacute;n</span></li>
                            <li class=""elementor-icon-list-item elementor-inline-item""><span class=""elementor-icon-list-icon""><i aria-hidden=""true"" class=""fas fa-paw""></i></span><span class=""elementor-icon-list-text"">Compromiso</span></li>
                            <li class=""elementor-icon-list-item elementor-inline-item""><span class=""elementor-icon-list-icon""><i aria-hidden=""true"" class=""fas fa-paw""></i></span><span class=""elementor-icon-list-text"">Competitividad</span></li>
                            <li class=""elementor-icon-list-item elementor-inline-item""><span class=""elementor-icon-list-icon""><i aria-hidden=""true"" class=""fas fa-paw""></i></span><span class=""elementor-icon-list-text"">Transparencia</span></li>
                            <li class=""elementor-icon-list-item elementor-inline-item""><span class=""elementor-icon-list-icon""><i aria-hidden=""true"" class=""fas fa-paw""></i></span><span class=""elementor-icon-list-text"">Trabajo en equipo</span></li>
                            <li class=""elementor-icon-list-item elementor-inline-item""><span class=""elementor-icon-list-icon""><i aria-hidden=""true"" class=""fas fa-paw""></i></span><span class=""elementor-icon-list-text"">Orientaci&oacute;n al cliente</span></li>
                            </ul>
                            </div>
                            </div>";
            Console.WriteLine(JsonConvert.SerializeObject(message).Trim());
        }

    }

}