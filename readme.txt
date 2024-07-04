instalirati ILMerge nuget paket
rebuildati solution u release konfiguraciji
u release folderu izvršiti:

../../../packages/ILMerge.3.0.41/tools/net452/ILMerge.exe /target:winexe /out:Comets-0.8.1.exe Comets.exe Comets.Application.Common.dll Comets.Application.Ephemeris.dll Comets.Application.Graph.dll Comets.Application.OrbitViewer.dll Comets.Core.dll Comets.OrbitViewer.dll
