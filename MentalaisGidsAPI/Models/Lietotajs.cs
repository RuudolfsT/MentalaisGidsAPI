﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MentalaisGidsAPI.Models;

public partial class Lietotajs
{
    public int LietotajsID { get; set; }

    public string Vards { get; set; }

    public string Uzvards { get; set; }

    public DateTime? DzimsanasGads { get; set; }

    public int? Dzimums { get; set; }

    public string Lietotajvards { get; set; }

    public string Epasts { get; set; }

    public byte[] Parole { get; set; }

    public bool Anonimitate { get; set; }

    public string GooglePilnvara { get; set; }

    public virtual ICollection<Dialogs> DialogsLietotajs { get; set; } = new List<Dialogs>();

    public virtual ICollection<Dialogs> DialogsSpecialists { get; set; } = new List<Dialogs>();

    public virtual ICollection<LietotajsLoma> LietotajsLoma { get; set; } = new List<LietotajsLoma>();

    public virtual ICollection<LietotajsRakstsKomentars> LietotajsRakstsKomentars { get; set; } = new List<LietotajsRakstsKomentars>();

    public virtual ICollection<LietotajsRakstsVertejums> LietotajsRakstsVertejums { get; set; } = new List<LietotajsRakstsVertejums>();

    public virtual ICollection<LietotajsSpecialists> LietotajsSpecialistsLietotajs { get; set; } = new List<LietotajsSpecialists>();

    public virtual ICollection<LietotajsSpecialists> LietotajsSpecialistsSpecialists { get; set; } = new List<LietotajsSpecialists>();

    public virtual ICollection<LietotajsSpecialistsVertejums> LietotajsSpecialistsVertejumsLietotajs { get; set; } = new List<LietotajsSpecialistsVertejums>();

    public virtual ICollection<LietotajsSpecialistsVertejums> LietotajsSpecialistsVertejumsSpecialists { get; set; } = new List<LietotajsSpecialistsVertejums>();

    public virtual ICollection<Nodarbiba> NodarbibaLietotajs { get; set; } = new List<Nodarbiba>();

    public virtual ICollection<Nodarbiba> NodarbibaSpecialists { get; set; } = new List<Nodarbiba>();

    public virtual ICollection<Raksts> Raksts { get; set; } = new List<Raksts>();

    public virtual ICollection<SajutuNovertejums> SajutuNovertejums { get; set; } = new List<SajutuNovertejums>();

    public virtual ICollection<Tests> Tests { get; set; } = new List<Tests>();

    public virtual ICollection<Zina> Zina { get; set; } = new List<Zina>();

    public virtual ICollection<Atbilde> Atbilde { get; set; } = new List<Atbilde>();

    public virtual ICollection<Nozare> Nozare { get; set; } = new List<Nozare>();
}