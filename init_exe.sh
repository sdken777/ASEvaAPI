CUR_DIR=$(cd $(dirname $0) && pwd)

dos2unix "$CUR_DIR"/export.sh
chmod -v +x "$CUR_DIR"/export.sh

dos2unix "$CUR_DIR"/gen_apidoc.sh
chmod -v +x "$CUR_DIR"/gen_apidoc.sh

chmod -v +x "$CUR_DIR"/binx/ASEvaAPIEtoTest
chmod -v +x "$CUR_DIR"/binxa/ASEvaAPIEtoTest

chmod -v +x "$CUR_DIR"/binx/TextResourceEditor
chmod -v +x "$CUR_DIR"/binxa/TextResourceEditor

cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/bin64/
cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/bin64/debug/
cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/binx/
cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/binx/debug/
cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/binxa/
cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/binxa/debug/
cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/binm/
cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/binm/debug/

cp -vf "$CUR_DIR"/3party/corewf-wpf/*.* "$CUR_DIR"/bin64/
cp -vf "$CUR_DIR"/3party/corewf-wpf/*.* "$CUR_DIR"/bin64/debug/

cp -vf "$CUR_DIR"/3party/eto/*.* "$CUR_DIR"/bin64/
cp -vf "$CUR_DIR"/3party/eto/*.* "$CUR_DIR"/bin64/debug/
cp -vf "$CUR_DIR"/3party/eto/*.* "$CUR_DIR"/binx/
cp -vf "$CUR_DIR"/3party/eto/*.* "$CUR_DIR"/binx/debug/
cp -vf "$CUR_DIR"/3party/eto/*.* "$CUR_DIR"/binxa/
cp -vf "$CUR_DIR"/3party/eto/*.* "$CUR_DIR"/binxa/debug/
cp -vf "$CUR_DIR"/3party/eto/*.* "$CUR_DIR"/binm/
cp -vf "$CUR_DIR"/3party/eto/*.* "$CUR_DIR"/binm/debug/

cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/bin64/
cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/bin64/debug/
cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/binx/
cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/binx/debug/
cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/binxa/
cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/binxa/debug/
cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/binm/
cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/binm/debug/

cp -vf "$CUR_DIR"/3party/gtk/*.* "$CUR_DIR"/binx/
cp -vf "$CUR_DIR"/3party/gtk/*.* "$CUR_DIR"/binx/debug/

cp -vf "$CUR_DIR"/3party/gtk/*.* "$CUR_DIR"/binxa/
cp -vf "$CUR_DIR"/3party/gtk/*.* "$CUR_DIR"/binxa/debug/

cp -vf "$CUR_DIR"/3party/linux-arm64-skia-native/*.* "$CUR_DIR"/binxa/
cp -vf "$CUR_DIR"/3party/linux-arm64-skia-native/*.* "$CUR_DIR"/binxa/debug/

cp -vf "$CUR_DIR"/3party/linux-x64-skia-native/*.* "$CUR_DIR"/binx/
cp -vf "$CUR_DIR"/3party/linux-x64-skia-native/*.* "$CUR_DIR"/binx/debug/

cp -vf "$CUR_DIR"/3party/macos/*.* "$CUR_DIR"/binm/
cp -vf "$CUR_DIR"/3party/macos/*.* "$CUR_DIR"/binm/debug/

cp -vf "$CUR_DIR"/3party/macos-skia-native/*.* "$CUR_DIR"/binm/
cp -vf "$CUR_DIR"/3party/macos-skia-native/*.* "$CUR_DIR"/binm/debug/

cp -vf "$CUR_DIR"/3party/monomac/*.* "$CUR_DIR"/binm/
cp -vf "$CUR_DIR"/3party/monomac/*.* "$CUR_DIR"/binm/debug/

cp -vf "$CUR_DIR"/3party/windows-skia-native/*.* "$CUR_DIR"/bin64/
cp -vf "$CUR_DIR"/3party/windows-skia-native/*.* "$CUR_DIR"/bin64/debug/

echo "Done. (ASEvaAPI/init_exe.sh)"
sleep 1
