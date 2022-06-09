CUR_DIR=$(dirname "$0")

dos2unix "$CUR_DIR"/export.sh
chmod -v +x "$CUR_DIR"/export.sh

dos2unix "$CUR_DIR"/export_essential.sh
chmod -v +x "$CUR_DIR"/export_essential.sh

dos2unix "$CUR_DIR"/gen_apidoc.sh
chmod -v +x "$CUR_DIR"/gen_apidoc.sh

chmod -v +x "$CUR_DIR"/binx/ASEvaAPIEtoTest
chmod -v +x "$CUR_DIR"/binxa/ASEvaAPIEtoTest

chmod -v +x "$CUR_DIR"/binx/TextResourceEditor
chmod -v +x "$CUR_DIR"/binxa/TextResourceEditor

echo "Done. (ASEvaAPI/init_exe.sh)"
sleep 1
