echo Beginning project build for Linux.
echo Be sure to have the Linux/Mono module installed.
'/c/Program Files/Unity/Hub/Editor/2020.3.5f1/Editor/Unity.exe' -quit -batchmode -logfile build.log -executeMethod MakeBuild.Linux
echo Build has finished. Check build.log for errors.