﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace TercerInventario.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? IdCategoria { get; set; }

    public virtual CategoriaProducto? IdCategoriaNavigation { get; set; }



        }