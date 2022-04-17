using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySet.Clases.Config
{
    public static class Sesion
    {
        public static string User {get; set;}
        public static string RolUser {get; set;}
        public static string PermissionUser {get; set;}
        private static string[] Permission = new string[3] {"Read","Write","Remove"};
        /*CONSULTA
         * SELECT user,permissions,permissions.description FROM `permissions` 
         * left join user_permissions on permissions.id = user_permissions.permissions_id 
         * left join usertype on usertype.id = user_permissions.userType_id LEFT join users on users.userType_id = usertype.id 
         * 
         */
        public static bool permissions(string[] permission)
        {
            bool result = true;
            if (permission[0] == Permission[0])
            {
                result = false;
                notifications.Messages.msj("NO TIENE PERMISOS DE LECTURA!");
            }else if(permission[1] == Permission[1])
            {
                result = false;
                notifications.Messages.msj("NO TIENE PERMISOS DE ESCRITURA!");

            }
            else if (permission[2] == Permission[2])
            {
                result = false;
                notifications.Messages.msj("NO TIENE PERMISOS PARA ELIMINAR!");

            }

            return result;
        }
    }
}
