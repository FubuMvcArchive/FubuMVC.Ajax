begin
  require 'bundler/setup'
  require 'fuburake'
rescue LoadError
  puts 'Bundler and all the gems need to be installed prior to running this rake script. Installing...'
  system("gem install bundler --source http://rubygems.org")
  sh 'bundle install'
  system("bundle exec rake", *ARGV)
  exit 0
end


FubuRake::Solution.new do |sln|
	sln.compile = {
		:solutionfile => 'src/FubuMVC.Ajax.sln'
	}
				 
	sln.assembly_info = {
		:product_name => "FubuMVC.Ajax",
		:copyright => 'Copyright 2012-2013 Josh Arnold, et al. All rights reserved.'
	}
	
	sln.assembly_bottle 'FubuMVC.Ajax'
	
	sln.ripple_enabled = true
	sln.fubudocs_enabled = true
end
