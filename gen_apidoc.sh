CUR_DIR=$(dirname "$0")

mkdir -vp ~/Desktop/D01006_ASEvaAPIDoc

cd $CUR_DIR/doc

doxygen Doxyfile-ASEvaAPI
cp -vf ./index.html ~/Desktop/D01006_ASEvaAPIDoc/Common/

doxygen Doxyfile-ASEvaAPICoreWF
cp -vf ./index.html ~/Desktop/D01006_ASEvaAPIDoc/CoreWF/

doxygen Doxyfile-ASEvaAPIGtk
cp -vf ./index.html ~/Desktop/D01006_ASEvaAPIDoc/Gtk/

doxygen Doxyfile-ASEvaAPIEto
cp -vf ./index.html ~/Desktop/D01006_ASEvaAPIDoc/Eto/

cd ~/Desktop
zip -r D01006_ASEvaAPIDoc.zip ./D01006_ASEvaAPIDoc/
rm -r ./D01006_ASEvaAPIDoc/

echo "Done. (ASEvaAPI/gen_apidoc.sh)"
sleep 1
