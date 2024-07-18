TARGET_NAME=ApiGuiSDK-Full

CUR_DIR=$(cd $(dirname $0) && pwd)
CUR_DATE=`date +%Y%m%d`

EXPORT_DEVELOPER=y
EXPORT_RUNTIME_DEBUG=y
EXPORT_GUI_LIBRARY=y
EXPORT_SKIA_NATIVE=n
EXPORT_AVALONIA=n
EXPORT_LIVECHARTS_WPF=n
EXPORT_LIVECHARTS_AVALONIA=n
if [ "$1" = "" ]; then
    TARGET_DIR=~/Desktop/$CUR_DATE-$TARGET_NAME
    GEN_DESKTOP_ZIP=y
else
    TARGET_DIR="$1"
    if [ "$2" = "runtime-with-debug" ]; then
        EXPORT_DEVELOPER=n
    fi
    if [ "$2" = "runtime-without-debug" ]; then
        EXPORT_DEVELOPER=n
        EXPORT_RUNTIME_DEBUG=n
    fi
    if [ "$2" = "runtime-with-debug-without-ui" ]; then
        EXPORT_DEVELOPER=n
        EXPORT_GUI_LIBRARY=n
    fi
    if [ "$2" = "runtime-without-debug-without-ui" ]; then
        EXPORT_DEVELOPER=n
        EXPORT_RUNTIME_DEBUG=n
        EXPORT_GUI_LIBRARY=n
    fi
    if [ "$EXPORT_GUI_LIBRARY" = "y" ]; then
        if [ "$3" = "with-skia-native" ]; then
            EXPORT_SKIA_NATIVE=y
            EXPORT_LIVECHARTS_WPF=y
        fi
        if [ "$3" = "with-avalonia" ]; then
            EXPORT_SKIA_NATIVE=y
            EXPORT_AVALONIA=y
            EXPORT_LIVECHARTS_WPF=y
            EXPORT_LIVECHARTS_AVALONIA=y
        fi
    fi
fi

mkdir -vp $TARGET_DIR/bin64
cp -vf "$CUR_DIR"/bin64/ASEvaAPI.dll $TARGET_DIR/bin64/
cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/bin64/
if [ "$EXPORT_GUI_LIBRARY" = "y" ]; then
    cp -vf "$CUR_DIR"/bin64/ASEvaAPIEto.dll $TARGET_DIR/bin64/
    cp -vf "$CUR_DIR"/bin64/ASEvaAPICoreWF.dll $TARGET_DIR/bin64/
    cp -vf "$CUR_DIR"/bin64/ASEvaAPIWpf.dll $TARGET_DIR/bin64/
    cp -vf "$CUR_DIR"/bin64/WinformWpfConverter.dll $TARGET_DIR/bin64/
    cp -vf "$CUR_DIR"/bin64/PortableSharpGL.dll $TARGET_DIR/bin64/
    cp -vf "$CUR_DIR"/3party/eto-common/* $TARGET_DIR/bin64/
    cp -vf "$CUR_DIR"/3party/eto-corewf-wpf/* $TARGET_DIR/bin64/
    cp -vf "$CUR_DIR"/3party/skia-common/* $TARGET_DIR/bin64/
    if [ "$EXPORT_SKIA_NATIVE" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/skia-windows-x64/* $TARGET_DIR/bin64/
    fi
    if [ "$EXPORT_AVALONIA" = "y" ]; then
        cp -vf "$CUR_DIR"/bin64/ASEvaAPIAvalonia.dll $TARGET_DIR/bin64/
        cp -vf "$CUR_DIR"/bin64/HwndHostAvalonia.dll $TARGET_DIR/bin64/
        cp -vf "$CUR_DIR"/3party/avalonia-common/* $TARGET_DIR/bin64/
        cp -vf "$CUR_DIR"/3party/avalonia-windows/* $TARGET_DIR/bin64/
    fi
    if [ "$EXPORT_LIVECHARTS_WPF" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/livecharts-common/* $TARGET_DIR/bin64/
        cp -vf "$CUR_DIR"/3party/livecharts-wpf/* $TARGET_DIR/bin64/
    fi
    if [ "$EXPORT_LIVECHARTS_AVALONIA" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/livecharts-avalonia/* $TARGET_DIR/bin64/
    fi
fi

mkdir -vp $TARGET_DIR/binx
cp -vf "$CUR_DIR"/binx/ASEvaAPI.dll $TARGET_DIR/binx/
cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/binx/
if [ "$EXPORT_GUI_LIBRARY" = "y" ]; then
    cp -vf "$CUR_DIR"/binx/ASEvaAPIEto.dll $TARGET_DIR/binx/
    cp -vf "$CUR_DIR"/binx/ASEvaAPIGtk.dll $TARGET_DIR/binx/
    cp -vf "$CUR_DIR"/binx/PortableSharpGL.dll $TARGET_DIR/binx/
    cp -vf "$CUR_DIR"/3party/eto-common/* $TARGET_DIR/binx/
    cp -vf "$CUR_DIR"/3party/eto-gtk/* $TARGET_DIR/binx/
    cp -vf "$CUR_DIR"/3party/skia-common/* $TARGET_DIR/binx/
    if [ "$EXPORT_SKIA_NATIVE" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/skia-linux-x64/* $TARGET_DIR/binx/
    fi
    if [ "$EXPORT_AVALONIA" = "y" ]; then
        cp -vf "$CUR_DIR"/binx/ASEvaAPIAvalonia.dll $TARGET_DIR/binx/
        cp -vf "$CUR_DIR"/binx/libXembedSocket.so $TARGET_DIR/binx/
        cp -vf "$CUR_DIR"/3party/avalonia-common/* $TARGET_DIR/binx/
        cp -vf "$CUR_DIR"/3party/avalonia-linux/* $TARGET_DIR/binx/
    fi
    if [ "$EXPORT_LIVECHARTS_AVALONIA" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/livecharts-common/* $TARGET_DIR/binx/
        cp -vf "$CUR_DIR"/3party/livecharts-avalonia/* $TARGET_DIR/binx/
    fi
fi

mkdir -vp $TARGET_DIR/binxa
cp -vf "$CUR_DIR"/binxa/ASEvaAPI.dll $TARGET_DIR/binxa/
cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/binxa/
if [ "$EXPORT_GUI_LIBRARY" = "y" ]; then
    cp -vf "$CUR_DIR"/binxa/ASEvaAPIEto.dll $TARGET_DIR/binxa/
    cp -vf "$CUR_DIR"/binxa/ASEvaAPIGtk.dll $TARGET_DIR/binxa/
    cp -vf "$CUR_DIR"/binxa/PortableSharpGL.dll $TARGET_DIR/binxa/
    cp -vf "$CUR_DIR"/3party/eto-common/* $TARGET_DIR/binxa/
    cp -vf "$CUR_DIR"/3party/eto-gtk/* $TARGET_DIR/binxa/
    cp -vf "$CUR_DIR"/3party/skia-common/* $TARGET_DIR/binxa/
    if [ "$EXPORT_SKIA_NATIVE" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/skia-linux-arm64/* $TARGET_DIR/binxa/
    fi
    if [ "$EXPORT_AVALONIA" = "y" ]; then
        cp -vf "$CUR_DIR"/binxa/ASEvaAPIAvalonia.dll $TARGET_DIR/binxa/
        cp -vf "$CUR_DIR"/binxa/libXembedSocket.so $TARGET_DIR/binxa/
        cp -vf "$CUR_DIR"/3party/avalonia-common/* $TARGET_DIR/binxa/
        cp -vf "$CUR_DIR"/3party/avalonia-linux/* $TARGET_DIR/binxa/
    fi
    if [ "$EXPORT_LIVECHARTS_AVALONIA" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/livecharts-common/* $TARGET_DIR/binxa/
        cp -vf "$CUR_DIR"/3party/livecharts-avalonia/* $TARGET_DIR/binxa/
    fi
fi

mkdir -vp $TARGET_DIR/binma
cp -vf "$CUR_DIR"/binma/ASEvaAPI.dll $TARGET_DIR/binma/
cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/binma/
if [ "$EXPORT_GUI_LIBRARY" = "y" ]; then
    cp -vf "$CUR_DIR"/binma/ASEvaAPIEto.dll $TARGET_DIR/binma/
    cp -vf "$CUR_DIR"/binma/ASEvaAPIMonoMac.dll $TARGET_DIR/binma/
    cp -vf "$CUR_DIR"/binma/PortableSharpGL.dll $TARGET_DIR/binma/
    cp -vf "$CUR_DIR"/3party/eto-common/* $TARGET_DIR/binma/
    cp -vf "$CUR_DIR"/3party/eto-monomac/* $TARGET_DIR/binma/
    cp -vf "$CUR_DIR"/3party/eto-monomac-arm64/* $TARGET_DIR/binma/
    cp -vf "$CUR_DIR"/3party/skia-common/* $TARGET_DIR/binma/
    if [ "$EXPORT_SKIA_NATIVE" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/skia-macos-any/* $TARGET_DIR/binma/
    fi
    if [ "$EXPORT_AVALONIA" = "y" ]; then
        cp -vf "$CUR_DIR"/binma/ASEvaAPIAvalonia.dll $TARGET_DIR/binma/
        cp -vf "$CUR_DIR"/3party/avalonia-common/* $TARGET_DIR/binma/
        cp -vf "$CUR_DIR"/3party/avalonia-macos/* $TARGET_DIR/binma/
    fi
    if [ "$EXPORT_LIVECHARTS_AVALONIA" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/livecharts-common/* $TARGET_DIR/binma/
        cp -vf "$CUR_DIR"/3party/livecharts-avalonia/* $TARGET_DIR/binma/
    fi
fi

if [ "$EXPORT_DEVELOPER" = "y" ]; then
    mkdir -vp $TARGET_DIR/bin64/debug
    cp -vf "$CUR_DIR"/bin64/ASEvaAPI.xml $TARGET_DIR/bin64/
    if [ "$EXPORT_GUI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/eto-doc/* $TARGET_DIR/bin64/
        cp -vf "$CUR_DIR"/3party/skia-doc/* $TARGET_DIR/bin64/
        cp -vf "$CUR_DIR"/bin64/ASEvaAPIEto.xml $TARGET_DIR/bin64/
        cp -vf "$CUR_DIR"/bin64/ASEvaAPICoreWF.xml $TARGET_DIR/bin64/
        cp -vf "$CUR_DIR"/bin64/ASEvaAPIWpf.xml $TARGET_DIR/bin64/
        if [ "$EXPORT_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/bin64/ASEvaAPIAvalonia.xml $TARGET_DIR/bin64/
        fi
        if [ "$EXPORT_LIVECHARTS_WPF" = "y" ]; then
            cp -vf "$CUR_DIR"/3party/livecharts-doc/* $TARGET_DIR/bin64/
        fi
    fi

    mkdir -vp $TARGET_DIR/binx/debug
    cp -vf "$CUR_DIR"/binx/ASEvaAPI.xml $TARGET_DIR/binx/
    if [ "$EXPORT_GUI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/eto-doc/* $TARGET_DIR/binx/
        cp -vf "$CUR_DIR"/3party/skia-doc/* $TARGET_DIR/binx/
        cp -vf "$CUR_DIR"/binx/ASEvaAPIEto.xml $TARGET_DIR/binx/
        cp -vf "$CUR_DIR"/binx/ASEvaAPIGtk.xml $TARGET_DIR/binx/
        if [ "$EXPORT_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/binx/ASEvaAPIAvalonia.xml $TARGET_DIR/binx/
        fi
        if [ "$EXPORT_LIVECHARTS_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/3party/livecharts-doc/* $TARGET_DIR/binx/
        fi
    fi

    mkdir -vp $TARGET_DIR/binxa/debug
    cp -vf "$CUR_DIR"/binxa/ASEvaAPI.xml $TARGET_DIR/binxa/
    if [ "$EXPORT_GUI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/eto-doc/* $TARGET_DIR/binxa/
        cp -vf "$CUR_DIR"/3party/skia-doc/* $TARGET_DIR/binxa/
        cp -vf "$CUR_DIR"/binxa/ASEvaAPIEto.xml $TARGET_DIR/binxa/
        cp -vf "$CUR_DIR"/binxa/ASEvaAPIGtk.xml $TARGET_DIR/binxa/
        if [ "$EXPORT_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/binxa/ASEvaAPIAvalonia.xml $TARGET_DIR/binxa/
        fi
        if [ "$EXPORT_LIVECHARTS_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/3party/livecharts-doc/* $TARGET_DIR/binxa/
        fi
    fi

    mkdir -vp $TARGET_DIR/binma/debug
    cp -vf "$CUR_DIR"/binma/ASEvaAPI.xml $TARGET_DIR/binma/
    if [ "$EXPORT_GUI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/eto-doc/* $TARGET_DIR/binma/
        cp -vf "$CUR_DIR"/3party/skia-doc/* $TARGET_DIR/binma/
        cp -vf "$CUR_DIR"/binma/ASEvaAPIEto.xml $TARGET_DIR/binma/
        cp -vf "$CUR_DIR"/binma/ASEvaAPIMonoMac.xml $TARGET_DIR/binma/
        if [ "$EXPORT_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/binma/ASEvaAPIAvalonia.xml $TARGET_DIR/binma/
        fi
        if [ "$EXPORT_LIVECHARTS_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/3party/livecharts-doc/* $TARGET_DIR/binma/
        fi
    fi
fi

if [ "$EXPORT_RUNTIME_DEBUG" = "y" ]; then
    mkdir -vp $TARGET_DIR/bin64/debug
    cp -vf "$CUR_DIR"/bin64/ASEvaAPI.dll $TARGET_DIR/bin64/debug/
    cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/bin64/debug/
    if [ "$EXPORT_GUI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/bin64/ASEvaAPIEto.dll $TARGET_DIR/bin64/debug/
        cp -vf "$CUR_DIR"/bin64/ASEvaAPICoreWF.dll $TARGET_DIR/bin64/debug/
        cp -vf "$CUR_DIR"/bin64/ASEvaAPIWpf.dll $TARGET_DIR/bin64/debug/
        cp -vf "$CUR_DIR"/bin64/WinformWpfConverter.dll $TARGET_DIR/bin64/debug/
        cp -vf "$CUR_DIR"/bin64/PortableSharpGL.dll $TARGET_DIR/bin64/debug/
        cp -vf "$CUR_DIR"/3party/eto-common/* $TARGET_DIR/bin64/debug/
        cp -vf "$CUR_DIR"/3party/eto-corewf-wpf/* $TARGET_DIR/bin64/debug/
        cp -vf "$CUR_DIR"/3party/skia-common/* $TARGET_DIR/bin64/debug/
        if [ "$EXPORT_SKIA_NATIVE" = "y" ]; then
            cp -vf "$CUR_DIR"/3party/skia-windows-x64/* $TARGET_DIR/bin64/debug/
        fi
        if [ "$EXPORT_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/bin64/ASEvaAPIAvalonia.dll $TARGET_DIR/bin64/debug/
            cp -vf "$CUR_DIR"/bin64/HwndHostAvalonia.dll $TARGET_DIR/bin64/debug/
            cp -vf "$CUR_DIR"/3party/avalonia-common/* $TARGET_DIR/bin64/debug/
            cp -vf "$CUR_DIR"/3party/avalonia-windows/* $TARGET_DIR/bin64/debug/
        fi
        if [ "$EXPORT_LIVECHARTS_WPF" = "y" ]; then
            cp -vf "$CUR_DIR"/3party/livecharts-common/* $TARGET_DIR/bin64/debug/
            cp -vf "$CUR_DIR"/3party/livecharts-wpf/* $TARGET_DIR/bin64/debug/
        fi
        if [ "$EXPORT_LIVECHARTS_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/3party/livecharts-avalonia/* $TARGET_DIR/bin64/debug/
        fi
    fi

    mkdir -vp $TARGET_DIR/binx/debug
    cp -vf "$CUR_DIR"/binx/ASEvaAPI.dll $TARGET_DIR/binx/debug/
    cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/binx/debug/
    if [ "$EXPORT_GUI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/binx/ASEvaAPIEto.dll $TARGET_DIR/binx/debug/
        cp -vf "$CUR_DIR"/binx/ASEvaAPIGtk.dll $TARGET_DIR/binx/debug/
        cp -vf "$CUR_DIR"/binx/PortableSharpGL.dll $TARGET_DIR/binx/debug/
        cp -vf "$CUR_DIR"/3party/eto-common/* $TARGET_DIR/binx/debug/
        cp -vf "$CUR_DIR"/3party/eto-gtk/* $TARGET_DIR/binx/debug/
        cp -vf "$CUR_DIR"/3party/skia-common/* $TARGET_DIR/binx/debug/
        if [ "$EXPORT_SKIA_NATIVE" = "y" ]; then
            cp -vf "$CUR_DIR"/3party/skia-linux-x64/* $TARGET_DIR/binx/debug/
        fi
        if [ "$EXPORT_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/binx/ASEvaAPIAvalonia.dll $TARGET_DIR/binx/debug/
            cp -vf "$CUR_DIR"/binx/libXembedSocket.so $TARGET_DIR/binx/debug/
            cp -vf "$CUR_DIR"/3party/avalonia-common/* $TARGET_DIR/binx/debug/
            cp -vf "$CUR_DIR"/3party/avalonia-linux/* $TARGET_DIR/binx/debug/
        fi
        if [ "$EXPORT_LIVECHARTS_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/3party/livecharts-common/* $TARGET_DIR/binx/debug/
            cp -vf "$CUR_DIR"/3party/livecharts-avalonia/* $TARGET_DIR/binx/debug/
        fi
    fi

    mkdir -vp $TARGET_DIR/binxa/debug
    cp -vf "$CUR_DIR"/binxa/ASEvaAPI.dll $TARGET_DIR/binxa/debug/
    cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/binxa/debug/
    if [ "$EXPORT_GUI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/binxa/ASEvaAPIEto.dll $TARGET_DIR/binxa/debug/
        cp -vf "$CUR_DIR"/binxa/ASEvaAPIGtk.dll $TARGET_DIR/binxa/debug/
        cp -vf "$CUR_DIR"/binxa/PortableSharpGL.dll $TARGET_DIR/binxa/debug/
        cp -vf "$CUR_DIR"/3party/eto-common/* $TARGET_DIR/binxa/debug/
        cp -vf "$CUR_DIR"/3party/eto-gtk/* $TARGET_DIR/binxa/debug/
        cp -vf "$CUR_DIR"/3party/skia-common/* $TARGET_DIR/binxa/debug/
        if [ "$EXPORT_SKIA_NATIVE" = "y" ]; then
            cp -vf "$CUR_DIR"/3party/skia-linux-arm64/* $TARGET_DIR/binxa/debug/
        fi
        if [ "$EXPORT_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/binxa/ASEvaAPIAvalonia.dll $TARGET_DIR/binxa/debug/
            cp -vf "$CUR_DIR"/binxa/libXembedSocket.so $TARGET_DIR/binxa/debug/
            cp -vf "$CUR_DIR"/3party/avalonia-common/* $TARGET_DIR/binxa/debug/
            cp -vf "$CUR_DIR"/3party/avalonia-linux/* $TARGET_DIR/binxa/debug/
        fi
        if [ "$EXPORT_LIVECHARTS_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/3party/livecharts-common/* $TARGET_DIR/binxa/debug/
            cp -vf "$CUR_DIR"/3party/livecharts-avalonia/* $TARGET_DIR/binxa/debug/
        fi
    fi

    mkdir -vp $TARGET_DIR/binma/debug
    cp -vf "$CUR_DIR"/binma/ASEvaAPI.dll $TARGET_DIR/binma/debug/
    cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/binma/debug/
    if [ "$EXPORT_GUI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/binma/ASEvaAPIEto.dll $TARGET_DIR/binma/debug/
        cp -vf "$CUR_DIR"/binma/ASEvaAPIMonoMac.dll $TARGET_DIR/binma/debug/
        cp -vf "$CUR_DIR"/binma/PortableSharpGL.dll $TARGET_DIR/binma/debug/
        cp -vf "$CUR_DIR"/3party/eto-common/* $TARGET_DIR/binma/debug/
        cp -vf "$CUR_DIR"/3party/eto-monomac/* $TARGET_DIR/binma/debug/
        cp -vf "$CUR_DIR"/3party/eto-monomac-arm64/* $TARGET_DIR/binma/debug/
        cp -vf "$CUR_DIR"/3party/skia-common/* $TARGET_DIR/binma/debug/
        if [ "$EXPORT_SKIA_NATIVE" = "y" ]; then
            cp -vf "$CUR_DIR"/3party/skia-macos-any/* $TARGET_DIR/binma/debug/
        fi
        if [ "$EXPORT_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/binma/ASEvaAPIAvalonia.dll $TARGET_DIR/binma/debug/
            cp -vf "$CUR_DIR"/3party/avalonia-common/* $TARGET_DIR/binma/debug/
            cp -vf "$CUR_DIR"/3party/avalonia-macos/* $TARGET_DIR/binma/debug/
        fi
        if [ "$EXPORT_LIVECHARTS_AVALONIA" = "y" ]; then
            cp -vf "$CUR_DIR"/3party/livecharts-common/* $TARGET_DIR/binma/debug/
            cp -vf "$CUR_DIR"/3party/livecharts-avalonia/* $TARGET_DIR/binma/debug/
        fi
    fi
fi

if [ "$GEN_DESKTOP_ZIP" = "y" ]; then
    cd ~/Desktop
    zip -r $CUR_DATE-$TARGET_NAME.zip ./$CUR_DATE-$TARGET_NAME/
    rm -r ./$CUR_DATE-$TARGET_NAME/
fi

echo "Done. (ASEvaAPI/export.sh)"
sleep 1
