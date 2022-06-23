TARGET_NAME=EtoSDK-Full

CUR_DIR=$(dirname "$0")
CUR_DATE=`date +%Y%m%d`

EXPORT_DEVELOPER=y
EXPORT_RUNTIME_DEBUG=y
EXPORT_UI_LIBRARY=y
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
        EXPORT_UI_LIBRARY=n
    fi
    if [ "$2" = "runtime-without-debug-without-ui" ]; then
        EXPORT_DEVELOPER=n
        EXPORT_RUNTIME_DEBUG=n
        EXPORT_UI_LIBRARY=n
    fi
fi

mkdir -vp $TARGET_DIR/bin64
cp -vf "$CUR_DIR"/bin64/ASEvaAPI.dll $TARGET_DIR/bin64/
cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/bin64/
if [ "$EXPORT_UI_LIBRARY" = "y" ]; then
    cp -vf "$CUR_DIR"/bin64/ASEvaAPIEto.dll $TARGET_DIR/bin64/
    cp -vf "$CUR_DIR"/bin64/ASEvaAPICoreWF.dll $TARGET_DIR/bin64/
    cp -vf "$CUR_DIR"/bin64/ASEvaAPIWpf.dll $TARGET_DIR/bin64/
    cp -vf "$CUR_DIR"/3party/eto/* $TARGET_DIR/bin64/
    cp -vf "$CUR_DIR"/3party/corewf-wpf/* $TARGET_DIR/bin64/
fi

mkdir -vp $TARGET_DIR/binx
cp -vf "$CUR_DIR"/binx/ASEvaAPI.dll $TARGET_DIR/binx/
cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/binx/
if [ "$EXPORT_UI_LIBRARY" = "y" ]; then
    cp -vf "$CUR_DIR"/binx/ASEvaAPIEto.dll $TARGET_DIR/binx/
    cp -vf "$CUR_DIR"/binx/ASEvaAPIGtk.dll $TARGET_DIR/binx/
    cp -vf "$CUR_DIR"/3party/eto/* $TARGET_DIR/binx/
    cp -vf "$CUR_DIR"/3party/gtk/* $TARGET_DIR/binx/
    cp -vf "$CUR_DIR"/3party/linux-x64/* $TARGET_DIR/binx/
fi

mkdir -vp $TARGET_DIR/binxa
cp -vf "$CUR_DIR"/binxa/ASEvaAPI.dll $TARGET_DIR/binxa/
cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/binxa/
if [ "$EXPORT_UI_LIBRARY" = "y" ]; then
    cp -vf "$CUR_DIR"/binxa/ASEvaAPIEto.dll $TARGET_DIR/binxa/
    cp -vf "$CUR_DIR"/binxa/ASEvaAPIGtk.dll $TARGET_DIR/binxa/
    cp -vf "$CUR_DIR"/3party/eto/* $TARGET_DIR/binxa/
    cp -vf "$CUR_DIR"/3party/gtk/* $TARGET_DIR/binxa/
    cp -vf "$CUR_DIR"/3party/linux-arm64/* $TARGET_DIR/binxa/
fi

mkdir -vp $TARGET_DIR/binm
cp -vf "$CUR_DIR"/binm/ASEvaAPI.dll $TARGET_DIR/binm/
cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/binm/
if [ "$EXPORT_UI_LIBRARY" = "y" ]; then
    cp -vf "$CUR_DIR"/binm/ASEvaAPIEto.dll $TARGET_DIR/binm/
    cp -vf "$CUR_DIR"/binm/ASEvaAPIMonoMac.dll $TARGET_DIR/binm/
    cp -vf "$CUR_DIR"/3party/eto/* $TARGET_DIR/binm/
    cp -vf "$CUR_DIR"/3party/monomac/* $TARGET_DIR/binm/
    cp -vf "$CUR_DIR"/3party/macos/* $TARGET_DIR/binm/
fi

if [ "$EXPORT_DEVELOPER" = "y" ]; then
    mkdir -vp $TARGET_DIR/bin64/debug
    cp -vf "$CUR_DIR"/bin64/ASEvaAPI.xml $TARGET_DIR/bin64/
    if [ "$EXPORT_UI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/eto-doc/* $TARGET_DIR/bin64/
        cp -vf "$CUR_DIR"/bin64/ASEvaAPIEto.xml $TARGET_DIR/bin64/
        cp -vf "$CUR_DIR"/bin64/ASEvaAPICoreWF.xml $TARGET_DIR/bin64/
        cp -vf "$CUR_DIR"/bin64/ASEvaAPIWpf.xml $TARGET_DIR/bin64/
    fi

    mkdir -vp $TARGET_DIR/binx/debug
    cp -vf "$CUR_DIR"/binx/ASEvaAPI.xml $TARGET_DIR/binx/
    if [ "$EXPORT_UI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/eto-doc/* $TARGET_DIR/binx/
        cp -vf "$CUR_DIR"/binx/ASEvaAPIEto.xml $TARGET_DIR/binx/
        cp -vf "$CUR_DIR"/binx/ASEvaAPIGtk.xml $TARGET_DIR/binx/
    fi

    mkdir -vp $TARGET_DIR/binxa/debug
    cp -vf "$CUR_DIR"/binxa/ASEvaAPI.xml $TARGET_DIR/binxa/
    if [ "$EXPORT_UI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/eto-doc/* $TARGET_DIR/binxa/
        cp -vf "$CUR_DIR"/binxa/ASEvaAPIEto.xml $TARGET_DIR/binxa/
        cp -vf "$CUR_DIR"/binxa/ASEvaAPIGtk.xml $TARGET_DIR/binxa/
    fi

    mkdir -vp $TARGET_DIR/binm/debug
    cp -vf "$CUR_DIR"/binm/ASEvaAPI.xml $TARGET_DIR/binm/
    if [ "$EXPORT_UI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/3party/eto-doc/* $TARGET_DIR/binm/
        cp -vf "$CUR_DIR"/binm/ASEvaAPIEto.xml $TARGET_DIR/binm/
        cp -vf "$CUR_DIR"/binm/ASEvaAPIMonoMac.xml $TARGET_DIR/binm/
    fi
fi

if [ "$EXPORT_RUNTIME_DEBUG" = "y" ]; then
    mkdir -vp $TARGET_DIR/bin64/debug
    cp -vf "$CUR_DIR"/bin64/ASEvaAPI.dll $TARGET_DIR/bin64/debug/
    cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/bin64/debug/
    if [ "$EXPORT_UI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/bin64/ASEvaAPIEto.dll $TARGET_DIR/bin64/debug/
        cp -vf "$CUR_DIR"/bin64/ASEvaAPICoreWF.dll $TARGET_DIR/bin64/debug/
        cp -vf "$CUR_DIR"/bin64/ASEvaAPIWpf.dll $TARGET_DIR/bin64/debug/
        cp -vf "$CUR_DIR"/3party/eto/* $TARGET_DIR/bin64/debug/
        cp -vf "$CUR_DIR"/3party/corewf-wpf/* $TARGET_DIR/bin64/debug/
    fi

    mkdir -vp $TARGET_DIR/binx/debug
    cp -vf "$CUR_DIR"/binx/ASEvaAPI.dll $TARGET_DIR/binx/debug/
    cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/binx/debug/
    if [ "$EXPORT_UI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/binx/ASEvaAPIEto.dll $TARGET_DIR/binx/debug/
        cp -vf "$CUR_DIR"/binx/ASEvaAPIGtk.dll $TARGET_DIR/binx/debug/
        cp -vf "$CUR_DIR"/3party/eto/* $TARGET_DIR/binx/debug/
        cp -vf "$CUR_DIR"/3party/gtk/* $TARGET_DIR/binx/debug/
        cp -vf "$CUR_DIR"/3party/linux-x64/* $TARGET_DIR/binx/debug/
    fi

    mkdir -vp $TARGET_DIR/binxa/debug
    cp -vf "$CUR_DIR"/binxa/ASEvaAPI.dll $TARGET_DIR/binxa/debug/
    cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/binxa/debug/
    if [ "$EXPORT_UI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/binxa/ASEvaAPIEto.dll $TARGET_DIR/binxa/debug/
        cp -vf "$CUR_DIR"/binxa/ASEvaAPIGtk.dll $TARGET_DIR/binxa/debug/
        cp -vf "$CUR_DIR"/3party/eto/* $TARGET_DIR/binxa/debug/
        cp -vf "$CUR_DIR"/3party/gtk/* $TARGET_DIR/binxa/debug/
        cp -vf "$CUR_DIR"/3party/linux-arm64/* $TARGET_DIR/binxa/debug/
    fi

    mkdir -vp $TARGET_DIR/binm/debug
    cp -vf "$CUR_DIR"/binm/ASEvaAPI.dll $TARGET_DIR/binm/debug/
    cp -vf "$CUR_DIR"/3party/common/* $TARGET_DIR/binm/debug/
    if [ "$EXPORT_UI_LIBRARY" = "y" ]; then
        cp -vf "$CUR_DIR"/binm/ASEvaAPIEto.dll $TARGET_DIR/binm/debug/
        cp -vf "$CUR_DIR"/binm/ASEvaAPIMonoMac.dll $TARGET_DIR/binm/debug/
        cp -vf "$CUR_DIR"/3party/eto/* $TARGET_DIR/binm/debug/
        cp -vf "$CUR_DIR"/3party/monomac/* $TARGET_DIR/binm/debug/
        cp -vf "$CUR_DIR"/3party/macos/* $TARGET_DIR/binm/debug/
    fi
fi

if [ "$GEN_DESKTOP_ZIP" = "y" ]; then
    cd ~/Desktop
    zip -r $CUR_DATE-$TARGET_NAME.zip ./$CUR_DATE-$TARGET_NAME/
    rm -r ./$CUR_DATE-$TARGET_NAME/
fi

echo "Done. (ASEvaAPI/export.sh)"
sleep 1
