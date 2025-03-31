using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finalproyecto
{ 
        public static class CargarImagen
        {
            
            public static void Imagen(PictureBox pbxImagen, string imagen)
            {
                try
                {
                    pbxImagen.Load(imagen); 
                }
                catch (Exception)
                {
                    
                    pbxImagen.Load("https://i0.wp.com/stretchingmexico.com/wp-content/uploads/2024/07/placeholder-1.webp?w=1200&quality=80&ssl=1");
                }
            }
        }
    }
