CUR_DIR=$(dirname "$0")
CUR_DATE=`date +%Y%m%d`

EXPORT_DEVELOPER=y
EXPORT_RUNTIME_DEBUG=y
if [ "$1" == "" ]; then
    TARGET_DIR=~/Desktop/$CUR_DATE-EtoSDK
    GEN_DESKTOP_ZIP=y
else
    TARGET_DIR="$1"
    if [ "$2" == "runtime-with-debug" ]; then
        EXPORT_DEVELOPER=n
    fi
    if [ "$2" == "runtime-without-debug" ]; then
        EXPORT_DEVELOPER=n
        EXPORT_RUNTIME_DEBUG=n
    fi
fi

mkdir -vp $TARGET_DIR/bin64
cp -vf $CUR_DIR/bin64/ASEvaAPI.dll $TARGET_DIR/bin64/
cp -vf $CUR_DIR/bin64/ASEvaAPIEto.dll $TARGET_DIR/bin64/
cp -vf $CUR_DIR/bin64/ASEvaAPICoreWF.dll $TARGET_DIR/bin64/
cp -vf $CUR_DIR/3party/common/* $TARGET_DIR/bin64/
cp -vf $CUR_DIR/3party/windows/* $TARGET_DIR/bin64/

mkdir -vp $TARGET_DIR/binx
cp -vf $CUR_DIR/binx/ASEvaAPI.dll $TARGET_DIR/binx/
cp -vf $CUR_DIR/binx/ASEvaAPIEto.dll $TARGET_DIR/binx/
cp -vf $CUR_DIR/binx/ASEvaAPIGtk.dll $TARGET_DIR/binx/
cp -vf $CUR_DIR/3party/common/* $TARGET_DIR/binx/
cp -vf $CUR_DIR/3party/linux/* $TARGET_DIR/binx/

mkdir -vp $TARGET_DIR/binxa
cp -vf $CUR_DIR/binxa/ASEvaAPI.dll $TARGET_DIR/binxa/
cp -vf $CUR_DIR/binxa/ASEvaAPIEto.dll $TARGET_DIR/binxa/
cp -vf $CUR_DIR/binxa/ASEvaAPIGtk.dll $TARGET_DIR/binxa/
cp -vf $CUR_DIR/3party/common/* $TARGET_DIR/binxa/
cp -vf $CUR_DIR/3party/linux/* $TARGET_DIR/binxa/

if [ "$EXPORT_DEVELOPER" == "y" ]; then
    cp -vf $CUR_DIR/bin64/ASEvaAPI.xml $TARGET_DIR/bin64/
    cp -vf $CUR_DIR/bin64/ASEvaAPIEto.xml $TARGET_DIR/bin64/
    cp -vf $CUR_DIR/bin64/ASEvaAPICoreWF.xml $TARGET_DIR/bin64/

    cp -vf $CUR_DIR/bin64/ASEvaAPI.xml $TARGET_DIR/bin64/debug/
    cp -vf $CUR_DIR/bin64/ASEvaAPIEto.xml $TARGET_DIR/bin64/debug/
    cp -vf $CUR_DIR/bin64/ASEvaAPICoreWF.xml $TARGET_DIR/bin64/debug/

    cp -vf $CUR_DIR/binx/ASEvaAPI.xml $TARGET_DIR/binx/
    cp -vf $CUR_DIR/binx/ASEvaAPIEto.xml $TARGET_DIR/binx/
    cp -vf $CUR_DIR/binx/ASEvaAPIGtk.xml $TARGET_DIR/binx/
fi

if [ "$EXPORT_RUNTIME_DEBUG" == "y" ]; then
    mkdir -vp $TARGET_DIR/bin64/debug
    cp -vf $CUR_DIR/bin64/ASEvaAPI.dll $TARGET_DIR/bin64/debug/
    cp -vf $CUR_DIR/bin64/ASEvaAPIEto.dll $TARGET_DIR/bin64/debug/
    cp -vf $CUR_DIR/bin64/ASEvaAPICoreWF.dll $TARGET_DIR/bin64/debug/
    cp -vf $CUR_DIR/3party/common/* $TARGET_DIR/bin64/debug/
    cp -vf $CUR_DIR/3party/windows/* $TARGET_DIR/bin64/debug/

    mkdir -vp $TARGET_DIR/binx/debug
    cp -vf $CUR_DIR/binx/ASEvaAPI.dll $TARGET_DIR/binx/debug/
    cp -vf $CUR_DIR/binx/ASEvaAPIEto.dll $TARGET_DIR/binx/debug/
    cp -vf $CUR_DIR/binx/ASEvaAPIGtk.dll $TARGET_DIR/binx/debug/
    cp -vf $CUR_DIR/3party/common/* $TARGET_DIR/binx/debug/
    cp -vf $CUR_DIR/3party/linux/* $TARGET_DIR/binx/debug/

    mkdir -vp $TARGET_DIR/binxa/debug
    cp -vf $CUR_DIR/binxa/ASEvaAPI.dll $TARGET_DIR/binxa/debug/
    cp -vf $CUR_DIR/binxa/ASEvaAPIEto.dll $TARGET_DIR/binxa/debug/
    cp -vf $CUR_DIR/binxa/ASEvaAPIGtk.dll $TARGET_DIR/binxa/debug/
    cp -vf $CUR_DIR/3party/common/* $TARGET_DIR/binxa/debug/
    cp -vf $CUR_DIR/3party/linux/* $TARGET_DIR/binxa/debug/
fi

if [ ! -z $GEN_DESKTOP_ZIP ]; then
    cd ~/Desktop
    zip -r $CUR_DATE-EtoSDK.zip ./$CUR_DATE-EtoSDK/
    rm -r ./$CUR_DATE-EtoSDK/
fi

sleep 3
