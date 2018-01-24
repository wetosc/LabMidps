using LAB7.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace LAB7.Data
{

    public static class DbInitializer
    {

        public static void Initialize(MyContext context)
        {
            context.Database.EnsureCreated();

            if (context.Wizard.Any()) { return; }

            var wizards = new Wizard[] {
                new Wizard{Name="Alefa", Color="grey"},
                new Wizard{Name="Betai", Color="black"}
            };
            var rings = new Ring[] {
                new Ring{Name="The ninth", Material="Gold"},
                new Ring{Name="The fifth", Material="Wood"},
                new Ring{Name="The fourth", Material="Silver"},
                new Ring{Name="The tenth", Material="Iron"},
            };

            wizards[0].WizardRings = new List<WizardRing> {
                new WizardRing{Wizard=wizards[0], Ring=rings[0]},
                new WizardRing{Wizard=wizards[0], Ring=rings[1]}
            };

            wizards[1].WizardRings = new List<WizardRing> {
                new WizardRing{Wizard=wizards[1], Ring=rings[2]},
                new WizardRing{Wizard=wizards[1], Ring=rings[3]}
            };

            foreach (Wizard w in wizards)
            {
                context.Wizard.Add(w);
            }
            foreach (Ring r in rings)
            {
                context.Ring.Add(r);
            }

            context.SaveChanges();
        }
    }

}