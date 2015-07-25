Given /^I am on the Home Screen$/ do
  home = page(HomeScreen).await
end

Then /^the part of speech label will be underlined$/ do
  element_exists("view")
  sleep(STEP_PAUSE)
end
