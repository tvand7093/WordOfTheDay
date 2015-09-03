Given /^I am on the Home Screen$/ do
  @home = page(HomeScreen).await
end

Then /^tapping the word opens the url$/ do
  @home.open_url
end
