CUR_DIR=$(cd $(dirname $0) && pwd)

dos2unix "$CUR_DIR"/export.sh
chmod -v +x "$CUR_DIR"/export.sh

dos2unix "$CUR_DIR"/gen_apidoc.sh
chmod -v +x "$CUR_DIR"/gen_apidoc.sh

chmod -v +x "$CUR_DIR"/binx/ASEvaAPIEtoTest
chmod -v +x "$CUR_DIR"/binxa/ASEvaAPIEtoTest
chmod -v +x "$CUR_DIR"/binma/ASEvaAPIEtoTest

chmod -v +x "$CUR_DIR"/binx/TextResourceEditor
chmod -v +x "$CUR_DIR"/binxa/TextResourceEditor
chmod -v +x "$CUR_DIR"/binma/TextResourceEditor

cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/bin64/
cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/bin64/debug/
cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/binx/
cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/binx/debug/
cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/binxa/
cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/binxa/debug/
cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/binma/
cp -vf "$CUR_DIR"/3party/common/*.* "$CUR_DIR"/binma/debug/

cp -vf "$CUR_DIR"/3party/eto-common/*.* "$CUR_DIR"/bin64/
cp -vf "$CUR_DIR"/3party/eto-common/*.* "$CUR_DIR"/bin64/debug/
cp -vf "$CUR_DIR"/3party/eto-common/*.* "$CUR_DIR"/binx/
cp -vf "$CUR_DIR"/3party/eto-common/*.* "$CUR_DIR"/binx/debug/
cp -vf "$CUR_DIR"/3party/eto-common/*.* "$CUR_DIR"/binxa/
cp -vf "$CUR_DIR"/3party/eto-common/*.* "$CUR_DIR"/binxa/debug/
cp -vf "$CUR_DIR"/3party/eto-common/*.* "$CUR_DIR"/binma/
cp -vf "$CUR_DIR"/3party/eto-common/*.* "$CUR_DIR"/binma/debug/

cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/bin64/
cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/bin64/debug/
cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/binx/
cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/binx/debug/
cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/binxa/
cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/binxa/debug/
cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/binma/
cp -vf "$CUR_DIR"/3party/eto-doc/*.* "$CUR_DIR"/binma/debug/

cp -vf "$CUR_DIR"/3party/eto-corewf-wpf/*.* "$CUR_DIR"/bin64/
cp -vf "$CUR_DIR"/3party/eto-corewf-wpf/*.* "$CUR_DIR"/bin64/debug/

cp -vf "$CUR_DIR"/3party/eto-gtk/*.* "$CUR_DIR"/binx/
cp -vf "$CUR_DIR"/3party/eto-gtk/*.* "$CUR_DIR"/binx/debug/

cp -vf "$CUR_DIR"/3party/eto-gtk/*.* "$CUR_DIR"/binxa/
cp -vf "$CUR_DIR"/3party/eto-gtk/*.* "$CUR_DIR"/binxa/debug/

cp -vf "$CUR_DIR"/3party/eto-monomac/*.* "$CUR_DIR"/binma/
cp -vf "$CUR_DIR"/3party/eto-monomac/*.* "$CUR_DIR"/binma/debug/

cp -vf "$CUR_DIR"/3party/skia-common/*.* "$CUR_DIR"/bin64/
cp -vf "$CUR_DIR"/3party/skia-common/*.* "$CUR_DIR"/bin64/debug/
cp -vf "$CUR_DIR"/3party/skia-common/*.* "$CUR_DIR"/binx/
cp -vf "$CUR_DIR"/3party/skia-common/*.* "$CUR_DIR"/binx/debug/
cp -vf "$CUR_DIR"/3party/skia-common/*.* "$CUR_DIR"/binxa/
cp -vf "$CUR_DIR"/3party/skia-common/*.* "$CUR_DIR"/binxa/debug/
cp -vf "$CUR_DIR"/3party/skia-common/*.* "$CUR_DIR"/binma/
cp -vf "$CUR_DIR"/3party/skia-common/*.* "$CUR_DIR"/binma/debug/

cp -vf "$CUR_DIR"/3party/skia-doc/*.* "$CUR_DIR"/bin64/
cp -vf "$CUR_DIR"/3party/skia-doc/*.* "$CUR_DIR"/bin64/debug/
cp -vf "$CUR_DIR"/3party/skia-doc/*.* "$CUR_DIR"/binx/
cp -vf "$CUR_DIR"/3party/skia-doc/*.* "$CUR_DIR"/binx/debug/
cp -vf "$CUR_DIR"/3party/skia-doc/*.* "$CUR_DIR"/binxa/
cp -vf "$CUR_DIR"/3party/skia-doc/*.* "$CUR_DIR"/binxa/debug/
cp -vf "$CUR_DIR"/3party/skia-doc/*.* "$CUR_DIR"/binma/
cp -vf "$CUR_DIR"/3party/skia-doc/*.* "$CUR_DIR"/binma/debug/

cp -vf "$CUR_DIR"/3party/skia-windows-x64/*.* "$CUR_DIR"/bin64/
cp -vf "$CUR_DIR"/3party/skia-windows-x64/*.* "$CUR_DIR"/bin64/debug/

cp -vf "$CUR_DIR"/3party/skia-linux-x64/*.* "$CUR_DIR"/binx/
cp -vf "$CUR_DIR"/3party/skia-linux-x64/*.* "$CUR_DIR"/binx/debug/

cp -vf "$CUR_DIR"/3party/skia-linux-arm64/*.* "$CUR_DIR"/binxa/
cp -vf "$CUR_DIR"/3party/skia-linux-arm64/*.* "$CUR_DIR"/binxa/debug/

cp -vf "$CUR_DIR"/3party/skia-macos-any/*.* "$CUR_DIR"/binma/
cp -vf "$CUR_DIR"/3party/skia-macos-any/*.* "$CUR_DIR"/binma/debug/

echo "Done. (ASEvaAPI/init_exe.sh)"
sleep 1
