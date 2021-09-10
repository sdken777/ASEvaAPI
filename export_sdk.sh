CUR_DIR=$(dirname "$0")
CUR_DATE=`date +%Y%m%d`
TARGET_DIR=~/Desktop/$CUR_DATE-EtoSDK

mkdir -vp $TARGET_DIR/bin64
cp -vf $CUR_DIR/bin64/ASEvaAPI.* $TARGET_DIR/bin64/
cp -vf $CUR_DIR/bin64/ASEvaAPIEto.* $TARGET_DIR/bin64/
cp -vf $CUR_DIR/bin64/ASEvaAPICoreWF.* $TARGET_DIR/bin64/
cp -vf $CUR_DIR/3party/common/* $TARGET_DIR/bin64/
cp -vf $CUR_DIR/3party/windows/* $TARGET_DIR/bin64/

mkdir -vp $TARGET_DIR/bin64/debug
cp -vf $CUR_DIR/bin64/ASEvaAPI.* $TARGET_DIR/bin64/debug/
cp -vf $CUR_DIR/bin64/ASEvaAPIEto.* $TARGET_DIR/bin64/debug/
cp -vf $CUR_DIR/bin64/ASEvaAPICoreWF.* $TARGET_DIR/bin64/debug/
cp -vf $CUR_DIR/3party/common/* $TARGET_DIR/bin64/debug/
cp -vf $CUR_DIR/3party/windows/* $TARGET_DIR/bin64/debug/

mkdir -vp $TARGET_DIR/binx
cp -vf $CUR_DIR/binx/ASEvaAPI.* $TARGET_DIR/binx/
cp -vf $CUR_DIR/binx/ASEvaAPIEto.* $TARGET_DIR/binx/
cp -vf $CUR_DIR/binx/ASEvaAPIGtk.* $TARGET_DIR/binx/
cp -vf $CUR_DIR/3party/common/* $TARGET_DIR/binx/
cp -vf $CUR_DIR/3party/linux/* $TARGET_DIR/binx/

mkdir -vp $TARGET_DIR/binx/debug
cp -vf $CUR_DIR/binx/ASEvaAPI.* $TARGET_DIR/binx/debug/
cp -vf $CUR_DIR/binx/ASEvaAPIEto.* $TARGET_DIR/binx/debug/
cp -vf $CUR_DIR/binx/ASEvaAPIGtk.* $TARGET_DIR/binx/debug/
cp -vf $CUR_DIR/3party/common/* $TARGET_DIR/binx/debug/
cp -vf $CUR_DIR/3party/linux/* $TARGET_DIR/binx/debug/

mkdir -vp $TARGET_DIR/binxa
cp -vf $CUR_DIR/binxa/ASEvaAPI.* $TARGET_DIR/binxa/
cp -vf $CUR_DIR/binxa/ASEvaAPIEto.* $TARGET_DIR/binxa/
cp -vf $CUR_DIR/binxa/ASEvaAPIGtk.* $TARGET_DIR/binxa/
cp -vf $CUR_DIR/3party/common/* $TARGET_DIR/binxa/
cp -vf $CUR_DIR/3party/linux/* $TARGET_DIR/binxa/

mkdir -vp $TARGET_DIR/binxa/debug
cp -vf $CUR_DIR/binxa/ASEvaAPI.* $TARGET_DIR/binxa/debug/
cp -vf $CUR_DIR/binxa/ASEvaAPIEto.* $TARGET_DIR/binxa/debug/
cp -vf $CUR_DIR/binxa/ASEvaAPIGtk.* $TARGET_DIR/binxa/debug/
cp -vf $CUR_DIR/3party/common/* $TARGET_DIR/binxa/debug/
cp -vf $CUR_DIR/3party/linux/* $TARGET_DIR/binxa/debug/

cd ~/Desktop
zip -r $CUR_DATE-EtoSDK.zip ./$CUR_DATE-EtoSDK/
rm -r ./$CUR_DATE-EtoSDK/

sleep 3
