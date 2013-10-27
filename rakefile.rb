require 'albacore'

task :default => ['build']

dir = File.dirname(__FILE__)
desc "build using msbuild"
msbuild :build do |msb|
  msb.properties :configuration => :Debug
  msb.targets :Clean, :Rebuild
  msb.verbosity = 'quiet'
  msb.solution =File.join(dir,"src", "ExtractPhoneNumbers.sln")
end

task :core_copy_to_nuspec => [:build] do
  output_directory_lib = File.join(dir,"nuget/lib/40/")
  mkdir_p output_directory_lib
  cp Dir.glob("./src/ExtractPhoneNumbers/bin/Debug/ExtractPhoneNumbers.dll"), output_directory_lib
  
end

desc "create the nuget package"
task :nugetpack => [:core_nugetpack]

task :core_nugetpack => [:core_copy_to_nuspec] do |nuget|
  cd File.join(dir,"nuget") do
    sh "..\\src\\.nuget\\NuGet.exe pack ExtractPhoneNumbers.nuspec"
  end
end


