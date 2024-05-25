
#ifndef __XEMBED_SOCKET_H__
#define __XEMBED_SOCKET_H__

#define XEMBED_SOCKET_API __attribute__ ((visibility("default")))

#include <X11/Xlib.h>

#ifdef  __cplusplus
extern "C" {
#endif

typedef struct _XembedSocketPrivate
{
    Display *x11_display;

    Window parent_x11_window;
    Window socket_x11_window;
    Window plug_x11_window;

    unsigned short current_width;
    unsigned short current_height;

    int configure_request_count;
    short xembed_version; // -1 == not xembed

    unsigned int cur_focus_in;
    unsigned int cur_active;
    
    unsigned int should_map;
    unsigned int map_request;
}
XembedSocketPrivate;

XEMBED_SOCKET_API XembedSocketPrivate *xembed_socket_create(Window parent_x11_window, int map);
XEMBED_SOCKET_API Window xembed_socket_get_socket_id(XembedSocketPrivate *context);
XEMBED_SOCKET_API int xembed_socket_handle_key (XembedSocketPrivate *context, int key_release, unsigned int time, unsigned int state, unsigned int keycode);
XEMBED_SOCKET_API void xembed_socket_get_size_request (XembedSocketPrivate *context, int *request_width, int *request_height);
XEMBED_SOCKET_API void xembed_socket_update_plug_allocation (XembedSocketPrivate *context);
XEMBED_SOCKET_API void xembed_socket_update_both_allocation (XembedSocketPrivate *context);
XEMBED_SOCKET_API void xembed_socket_handle_request (XembedSocketPrivate *context);
XEMBED_SOCKET_API int xembed_socket_filter_func (XembedSocketPrivate *context, XEvent *x11_event, int *queue_resize);
XEMBED_SOCKET_API void xembed_socket_update_focus_in (XembedSocketPrivate *context, int focus_in);
XEMBED_SOCKET_API void xembed_socket_update_active (XembedSocketPrivate *context, int active);
XEMBED_SOCKET_API int xembed_socket_next_event (XembedSocketPrivate *context, XEvent *x11_event);
XEMBED_SOCKET_API void xembed_socket_iteration (XembedSocketPrivate *context, int focus_in, int active);

#ifdef  __cplusplus
}
#endif

#endif /* __XEMBED_SOCKET_H__ */
