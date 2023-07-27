CUR_DIR=$(dirname "$0")

mkdir -vp ~/Desktop/D01006_ASEvaAPIDoc

cd "$CUR_DIR"/doc

doxygen Doxyfile-ASEvaAPI
cp -vf ./index.html ~/Desktop/D01006_ASEvaAPIDoc/Common/

doxygen Doxyfile-ASEvaAPIEto
cp -vf ./index.html ~/Desktop/D01006_ASEvaAPIDoc/Eto/

doxygen Doxyfile-ASEvaAPICoreWF
cp -vf ./index.html ~/Desktop/D01006_ASEvaAPIDoc/CoreWF/

doxygen Doxyfile-ASEvaAPIWpf
cp -vf ./index.html ~/Desktop/D01006_ASEvaAPIDoc/Wpf/

doxygen Doxyfile-ASEvaAPIGtk
cp -vf ./index.html ~/Desktop/D01006_ASEvaAPIDoc/Gtk/

doxygen Doxyfile-ASEvaAPIMonoMac
cp -vf ./index.html ~/Desktop/D01006_ASEvaAPIDoc/MonoMac/

echo "Done. (ASEvaAPI/gen_apidoc.sh)"
sleep 1
