
#include "xembed_socket.h"
#include "xembed.h"
#include <X11/Xutil.h>
#include <stdlib.h>
#include <memory.h>
#include <stdio.h>

#define MAX(a, b) ((a) > (b) ? (a) : (b))
#define MIN(a, b) ((a) < (b) ? (a) : (b))

void reset_private (XembedSocketPrivate *private, int first);
void add_window (XembedSocketPrivate *socket, Window plug_x11_window);

XembedSocketPrivate *xembed_socket_create(Window parent_x11_window, int map)
{
    if (!parent_x11_window) return 0;

    Display *x11_display = XOpenDisplay(0);

    XembedSocketPrivate *private = (XembedSocketPrivate*)malloc(sizeof(XembedSocketPrivate));
    reset_private(private, 1);

    XWindowAttributes rootWindowAttributes;
    XGetWindowAttributes(x11_display, parent_x11_window, &rootWindowAttributes);

    XSetWindowAttributes setWindowAttributes;
    setWindowAttributes.override_redirect = True;
    setWindowAttributes.event_mask = FocusChangeMask;
    Window socket_x11_window = XCreateWindow(x11_display, parent_x11_window, 0, 0, 1, 1, 0, CopyFromParent, CopyFromParent, CopyFromParent, CWOverrideRedirect | CWEventMask, &setWindowAttributes);
    // printf("%s\n", "socket window created");

    XSelectInput (x11_display, socket_x11_window, 0x005b8000);
    if (map) XMapWindow(x11_display, socket_x11_window);
    XSync(x11_display, 0);

    private->x11_display = x11_display;
    private->parent_x11_window = parent_x11_window;
    private->socket_x11_window = socket_x11_window;
    return private;
}

Window xembed_socket_get_socket_id(XembedSocketPrivate *private)
{
    if (!private) return 0;
    else return private->socket_x11_window;
}

int xembed_socket_handle_key (XembedSocketPrivate *private, int key_release, unsigned int time, unsigned int state, unsigned int keycode)
{
    if (!private) return 0;
    if (!private->plug_x11_window) return 0;

    XKeyEvent xkey;
    memset (&xkey, 0, sizeof (xkey));
    xkey.type = key_release ? KeyRelease : KeyPress;
    xkey.window = private->plug_x11_window;
    xkey.root = DefaultRootWindow(private->x11_display);
    xkey.subwindow = None;
    xkey.time = time;
    xkey.x = 0;
    xkey.y = 0;
    xkey.x_root = 0;
    xkey.y_root = 0;
    xkey.state = state;
    xkey.keycode = keycode;
    xkey.same_screen = 1;
    XSendEvent (private->x11_display, private->plug_x11_window, 0, NoEventMask, (XEvent *)&xkey);
    return 1;
}

void xembed_socket_get_size_request (XembedSocketPrivate *private, int *request_width, int *request_height)
{
    if (!private || !request_width || !request_height) return;

    *request_width = *request_height = 1;
    if (!private->should_map || !private->plug_x11_window) return;

    XSizeHints hints;
    long dummy;
    if (XGetWMNormalHints (private->x11_display, private->plug_x11_window, &hints, &dummy))
    {
        if (hints.flags & PMinSize)
        {
            *request_width = hints.min_width;
            *request_height = hints.min_height;
        }
        else if (hints.flags & PBaseSize)
        {
            *request_width = hints.base_width;
            *request_height = hints.base_height;
        }
    }
}

void xembed_socket_update_plug_allocation (XembedSocketPrivate *private)
{
    if (!private) return;
    if (!private->socket_x11_window || !private->plug_x11_window) return;

    XWindowAttributes attribs;
    XGetWindowAttributes(private->x11_display, private->socket_x11_window, &attribs);

    if (attribs.width != private->current_width || attribs.height != private->current_height)
    {
        XMoveResizeWindow(private->x11_display, private->plug_x11_window, 0, 0, attribs.width, attribs.height);
        if (private->configure_request_count)
        {
            private->configure_request_count--;
        }
        private->current_width = attribs.width;
        private->current_height = attribs.height;
    }
}

void xembed_socket_update_both_allocation(XembedSocketPrivate *private)
{
    if (!private) return;
    if (!private->parent_x11_window || !private->socket_x11_window) return;

    XWindowAttributes attribs;
    XGetWindowAttributes(private->x11_display, private->parent_x11_window, &attribs);

    int request_width = 0, request_height = 0;
    xembed_socket_get_size_request(private, &request_width, &request_height);

    XMoveResizeWindow(private->x11_display, private->socket_x11_window, 0, 0, MAX (request_width, attribs.width), MAX (request_height, attribs.height));
    xembed_socket_update_plug_allocation(private);
}

void xembed_socket_handle_request (XembedSocketPrivate *private)
{
    if (!private) return;
    if (!private->plug_x11_window) return;

    if (private->map_request)
    {
        XMapWindow(private->x11_display, private->plug_x11_window);
        private->map_request = 0;
    }

    while (private->configure_request_count)
    {
        XWindowAttributes attribs;
        XGetWindowAttributes(private->x11_display, private->plug_x11_window, &attribs);

        XConfigureEvent xconfigure;
        memset (&xconfigure, 0, sizeof (xconfigure));
        xconfigure.type = ConfigureNotify;
        xconfigure.event = private->plug_x11_window;
        xconfigure.window = private->plug_x11_window;
        xconfigure.x = attribs.x;
        xconfigure.y = attribs.y;
        xconfigure.width = attribs.width;
        xconfigure.height = attribs.height;
        xconfigure.border_width = 0;
        xconfigure.above = None;
        xconfigure.override_redirect = 0;
        XSendEvent (private->x11_display, private->plug_x11_window, 0, NoEventMask, (XEvent *)&xconfigure);

        private->configure_request_count--;
    }
}

void reset_private (XembedSocketPrivate *private, int first)
{
    if (!private) return;

    if (!first)
    {
        if (private->socket_x11_window)
        {
            XDestroyWindow(private->x11_display, private->socket_x11_window);
            // printf("%s\n", "socket window destroyed");
        }
        if (private->x11_display) XCloseDisplay(private->x11_display);
    }

    private->x11_display = NULL;
    private->parent_x11_window = private->socket_x11_window = private->plug_x11_window = 0;

    private->current_width = private->current_height = 0;

    private->configure_request_count = 0;
    private->xembed_version = -1;

    private->cur_focus_in = 0;
    private->cur_active = 0;
    
    private->map_request = 0;
    private->should_map = 0;
}

void xembed_socket_update_focus_in (XembedSocketPrivate *private, int focus_in)
{
    if (!private) return;

    if (!private->plug_x11_window) focus_in = 0;
    if (focus_in != private->cur_focus_in)
    {
        private->cur_focus_in = focus_in;
        if (private->plug_x11_window)
        {
            if (focus_in) _xembed_send_message (private->x11_display, private->plug_x11_window, XEMBED_FOCUS_IN, XEMBED_FOCUS_CURRENT, 0, 0);
            else _xembed_send_message (private->x11_display, private->plug_x11_window, XEMBED_FOCUS_OUT, 0, 0, 0);
        }
    }
}

void xembed_socket_update_active (XembedSocketPrivate *private, int active)
{
    if (!private) return;

    if (!private->plug_x11_window) active = 0;
    if (active != private->cur_active)
    {
        private->cur_active = active;
        if (private->plug_x11_window) _xembed_send_message (private->x11_display, private->plug_x11_window, active ? XEMBED_WINDOW_ACTIVATE : XEMBED_WINDOW_DEACTIVATE, 0, 0, 0);
    }
}

void add_window (XembedSocketPrivate *private, Window plug_x11_window)
{
    if (!private || !plug_x11_window) return;

    XSelectInput (private->x11_display, plug_x11_window, StructureNotifyMask | PropertyChangeMask);

    unsigned long version;
    unsigned long flags;
    if (_xembed_get_info (private->x11_display, plug_x11_window, &version, &flags))
    {
        private->xembed_version = MIN (XEMBED_PROTOCOL_VERSION, version);
        private->should_map = (flags & XEMBED_MAPPED) != 0;
    }
    else private->should_map = 1;

    private->map_request = private->should_map;
    private->plug_x11_window = plug_x11_window;

    _xembed_send_message (private->x11_display, private->plug_x11_window, XEMBED_EMBEDDED_NOTIFY, 0, private->socket_x11_window, private->xembed_version);
}

int xembed_socket_filter_func (XembedSocketPrivate *private, XEvent *x11_event, int *queue_resize)
{
    if (!private || !queue_resize) return 0;

    switch (x11_event->type)
    {
    case CreateNotify:
        if (private->socket_x11_window && x11_event->xcreatewindow.parent == private->socket_x11_window)
        {
            if (!private->plug_x11_window)
            {
                add_window (private, x11_event->xcreatewindow.window);
                *queue_resize = 1;
            }
            return 1;
        }
        break;        

    case ReparentNotify:
        if (private->socket_x11_window && x11_event->xreparent.parent == private->socket_x11_window)
        {
            if (!private->plug_x11_window)
            {
                add_window (private, x11_event->xreparent.window);
                *queue_resize = 1;
            }
            return 1;
        }
        break;
        
    case ConfigureRequest:
        if (private->plug_x11_window && x11_event->xconfigurerequest.window == private->plug_x11_window)
        {
            private->configure_request_count++;
            *queue_resize = 1;
            return 1;
        }
        break;

    case PropertyNotify:
        if (private->plug_x11_window && x11_event->xproperty.window == private->plug_x11_window)
        {
            unsigned long flags;
            if (x11_event->xproperty.atom == XInternAtom(private->x11_display, "_XEMBED_INFO", 0) && 
                _xembed_get_info (private->x11_display, private->plug_x11_window, NULL, &flags))
            {
                int new_should_map = (flags & XEMBED_MAPPED) != 0;
                if (new_should_map && !private->should_map)
                {
                    private->should_map = 1;
                    private->map_request = 1;
                    *queue_resize = 1;
                }
                
            }
            return 1;
        }
        break;

    case UnmapNotify:
        if (private->plug_x11_window && x11_event->xunmap.window == private->plug_x11_window)
        {
            if (private->should_map)
            {
                private->should_map = 0;
                *queue_resize = 1;
            }
            return 1;
        }
        break;

    case DestroyNotify:
        if (private->plug_x11_window && x11_event->xdestroywindow.window == private->plug_x11_window)
        {
            reset_private(private, 0);
            return 1;
        }
        break;
    }
    return 0;
}

int xembed_socket_next_event(XembedSocketPrivate *private, XEvent *x11_event)
{
    if (!private || !private->x11_display) return 0;

    if (XPending(private->x11_display))
    {
        XNextEvent(private->x11_display, x11_event);
        return 1;
    }
    else return 0;
}

void xembed_socket_iteration(XembedSocketPrivate *private, int focus_in, int active)
{
    if (!private || !private->x11_display) return;

    XSync(private->x11_display, 0);

    XEvent x11_event;
    while (private->x11_display && XPending(private->x11_display))
    {
        XNextEvent(private->x11_display, &x11_event);

        int queue_resize = 0;
        if (xembed_socket_filter_func(private, &x11_event, &queue_resize))
        {
            if (queue_resize)
            {
                xembed_socket_update_plug_allocation(private);
                xembed_socket_handle_request(private);
                xembed_socket_update_focus_in(private, focus_in);
                xembed_socket_update_active(private, active);
            }
            continue;
        }
    }
}