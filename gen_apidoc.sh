CUR_DIR=$(cd $(dirname $0) && pwd)

mkdir -vp ~/Desktop/D01010_ASEvaAPIDoc_Chinese
mkdir -vp ~/Desktop/D01010_ASEvaAPIDoc_English

cd "$CUR_DIR"/doc

DOCUMENT_LANGUAGE=Chinese doxygen Doxyfile-ASEvaAPI
cp -vf ./index.html ~/Desktop/D01010_ASEvaAPIDoc_Chinese/Common/
DOCUMENT_LANGUAGE=English doxygen Doxyfile-ASEvaAPI
cp -vf ./index.html ~/Desktop/D01010_ASEvaAPIDoc_English/Common/

DOCUMENT_LANGUAGE=Chinese doxygen Doxyfile-ASEvaAPIEto
cp -vf ./index.html ~/Desktop/D01010_ASEvaAPIDoc_Chinese/Eto/
DOCUMENT_LANGUAGE=English doxygen Doxyfile-ASEvaAPIEto
cp -vf ./index.html ~/Desktop/D01010_ASEvaAPIDoc_English/Eto/

DOCUMENT_LANGUAGE=Chinese doxygen Doxyfile-ASEvaAPICoreWF
cp -vf ./index.html ~/Desktop/D01010_ASEvaAPIDoc_Chinese/CoreWF/
DOCUMENT_LANGUAGE=English doxygen Doxyfile-ASEvaAPICoreWF
cp -vf ./index.html ~/Desktop/D01010_ASEvaAPIDoc_English/CoreWF/

DOCUMENT_LANGUAGE=Chinese doxygen Doxyfile-ASEvaAPIWpf
cp -vf ./index.html ~/Desktop/D01010_ASEvaAPIDoc_Chinese/Wpf/
DOCUMENT_LANGUAGE=English doxygen Doxyfile-ASEvaAPIWpf
cp -vf ./index.html ~/Desktop/D01010_ASEvaAPIDoc_English/Wpf/

DOCUMENT_LANGUAGE=Chinese doxygen Doxyfile-ASEvaAPIGtk
cp -vf ./index.html ~/Desktop/D01010_ASEvaAPIDoc_Chinese/Gtk/
DOCUMENT_LANGUAGE=English doxygen Doxyfile-ASEvaAPIGtk
cp -vf ./index.html ~/Desktop/D01010_ASEvaAPIDoc_English/Gtk/

DOCUMENT_LANGUAGE=Chinese doxygen Doxyfile-ASEvaAPIMonoMac
cp -vf ./index.html ~/Desktop/D01010_ASEvaAPIDoc_Chinese/MonoMac/
DOCUMENT_LANGUAGE=English doxygen Doxyfile-ASEvaAPIMonoMac
cp -vf ./index.html ~/Desktop/D01010_ASEvaAPIDoc_English/MonoMac/

echo "Done. (ASEvaAPI/gen_apidoc.sh)"
sleep 1
