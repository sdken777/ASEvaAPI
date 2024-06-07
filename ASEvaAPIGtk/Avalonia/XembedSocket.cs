using System;
using System.Runtime.InteropServices;

namespace ASEva.UIGtk
{
    class XembedSocket
    {
        [DllImport("libXembedSocket.so")]
        public static extern nint xembed_socket_create(uint parent, int map);

        [DllImport("libXembedSocket.so")]
        public static extern void xembed_socket_update_both_allocation(nint socket);

        [DllImport("libXembedSocket.so")]
        public static extern void xembed_socket_iteration(nint socket, int focus, int active);

        [DllImport("libXembedSocket.so")]
        public static extern uint xembed_socket_get_socket_id(nint socket);

        [DllImport("libXembedSocket.so")]
        public static extern void xembed_socket_update_focus_in(nint socket, int focus);

        [DllImport("libXembedSocket.so")]
        public static extern void xembed_socket_update_active(nint socket, int active);
    }
}