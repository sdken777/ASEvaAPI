
#include "xembed.h"
#include <stdlib.h>
#include <memory.h>

typedef struct _XEmbedMessage XEmbedMessage;
struct _XEmbedMessage
{
    long message;
    long detail;
    long data1;
    long data2;
    unsigned int time;
    XEmbedMessage *next;
};
XEmbedMessage *first_message = 0;

void _xembed_send_message (Display *x11Display, Window x11Window, XEmbedMessageType message, long detail, long data1, long data2)
{
    XClientMessageEvent xclient;
    memset (&xclient, 0, sizeof (xclient));
    xclient.window = x11Window;
    xclient.type = ClientMessage;
    xclient.message_type = XInternAtom(x11Display, "_XEMBED", 0);
    xclient.format = 32;
    xclient.data.l[0] = CurrentTime;
    xclient.data.l[1] = message;
    xclient.data.l[2] = detail;
    xclient.data.l[3] = data1;
    xclient.data.l[4] = data2;

    XSendEvent (x11Display, x11Window, 0, NoEventMask, (XEvent *)&xclient);
}

int _xembed_get_info (Display *x11Display, Window x11Window, unsigned long *version, unsigned long *flags)
{
    Atom xembed_info_atom = XInternAtom (x11Display, "_XEMBED_INFO", 0);

    Atom type;
    int format;
    unsigned long nitems, bytes_after;
    unsigned char *data;
    int status = XGetWindowProperty (x11Display, x11Window, xembed_info_atom, 0, 2, 0, xembed_info_atom, &type, &format, &nitems, &bytes_after, &data);

    if (status != Success) return 0; // Window vanished?
    if (type == None) return 0; // No info property
    if (type != xembed_info_atom) return 0;
    
    if (nitems < 2)
    {
        XFree (data);
        return 0;
    }
  
    unsigned long *data_long = (unsigned long *)data;
    if (version) *version = data_long[0];
    if (flags) *flags = data_long[1] & XEMBED_MAPPED;
    
    XFree (data);
    return 1;
}