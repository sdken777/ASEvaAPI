﻿using NUnit.Framework;
namespace Eto.Test.UnitTests.Forms
{
	/// <summary>
	/// Tests to ensure the DataContext and DataContextChanged event act appropriately
	/// - DataContextChanged should only be fired max once per control, regardless of how or when they are constructed and added to the tree
	/// - Themed controls (visual tree) should not participate in logical tree data context
	/// </summary>
	[TestFixture]
	public class DataContextTests : TestBase
	{
		[Handler(typeof(IHandler))]
		public class CustomExpander : Expander
		{
			public new interface IHandler : Expander.IHandler { }
		}

		public class CustomExpanderHandler : Eto.Forms.ThemedControls.ThemedExpanderHandler, CustomExpander.IHandler
		{
			int dataContextChanged;
			int contentDataContextChanged;
			Panel content;

			class MyViewModel2
			{
			}

			public override Control Content
			{
				get { return content.Content; }
				set { content.Content = value; }
			}

			protected override void Initialize()
			{
				base.Initialize();

				content = new Panel();
				Control.DataContextChanged += (sender, e) => dataContextChanged++;
				content.DataContextChanged += (sender, e) => contentDataContextChanged++;

				base.Content = content;

				Assert.That(dataContextChanged, Is.EqualTo(0));
				Assert.That(contentDataContextChanged, Is.EqualTo(0));
				Control.DataContext = new MyViewModel2(); // this shouldn't fire data context changes for logical children
				Assert.That(dataContextChanged, Is.EqualTo(1));
				Assert.That(contentDataContextChanged, Is.EqualTo(1));
			}

			public override void OnLoad(EventArgs e)
			{
				base.OnLoad(e);
				Assert.That(Control.DataContext, Is.InstanceOf<MyViewModel2>());
				Assert.That(content.DataContext, Is.InstanceOf<MyViewModel2>());

				Control.DataContext = new MyViewModel2(); // this shouldn't fire data context changes for logical children
				Assert.That(dataContextChanged, Is.EqualTo(2));
				Assert.That(contentDataContextChanged, Is.EqualTo(2));
				Assert.That(Control.DataContext, Is.InstanceOf<MyViewModel2>());
				Assert.That(content.DataContext, Is.InstanceOf<MyViewModel2>());
			}

			public override void OnLoadComplete(EventArgs e)
			{
				base.OnLoadComplete(e);
				Assert.That(dataContextChanged, Is.EqualTo(2));
				Assert.That(contentDataContextChanged, Is.EqualTo(2));
				Assert.That(Control.DataContext, Is.InstanceOf<MyViewModel2>());
				Assert.That(content.DataContext, Is.InstanceOf<MyViewModel2>());
			}
		}

		static DataContextTests()
		{
			Platform.Instance.Add<CustomExpander.IHandler>(() => new CustomExpanderHandler());
		}

		public class MyViewModel
		{
			public int ID { get; set; }
		}

		public class MyViewModelWithEquals
		{
			public int ID { get; set; }

			public override bool Equals(object obj)
			{
				var model = obj as MyViewModelWithEquals;
				if (model == null)
					return false;

				return ID.Equals(model.ID);
			}

			public override int GetHashCode()
			{
				return ID.GetHashCode();
			}
		}

		[Test]
		public void DataContextChangedShouldNotFireWhenNoContext()
		{
			int dataContextChanged = 0;
			Shown(form =>
			{
				form.DataContextChanged += (sender, e) => dataContextChanged++;
				var c = new Panel();
				c.DataContextChanged += (sender, e) => dataContextChanged++;
				form.Content = c;
				Assert.That(dataContextChanged, Is.EqualTo(0));
				Assert.That(form.DataContext, Is.Null);
				Assert.That(c.DataContext, Is.Null);
			}, () =>
			{
				Assert.That(dataContextChanged, Is.EqualTo(0));
			});
		}

		[Test]
		public void DataContextChangedShouldFireAfterSet()
		{
			int dataContextChanged = 0;
			MyViewModel dataContext;
			Shown(form =>
			{
				var c = new Panel();
				c.DataContextChanged += (sender, e) => dataContextChanged++;
				c.DataContext = dataContext = new MyViewModel();
				Assert.That(dataContextChanged, Is.EqualTo(1));
				Assert.That(c.DataContext, Is.InstanceOf<MyViewModel>());
				Assert.That(dataContext, Is.SameAs(c.DataContext));

				c.DataContext = dataContext = new MyViewModel();
				Assert.That(dataContextChanged, Is.EqualTo(2));
				Assert.That(c.DataContext, Is.InstanceOf<MyViewModel>());
				Assert.That(dataContext, Is.SameAs(c.DataContext));

				form.Content = c;
				Assert.That(dataContextChanged, Is.EqualTo(2));
			}, () =>
			{
				Assert.That(dataContextChanged, Is.EqualTo(2));
			});
		}

		[Test]
		public void DataContextChangedShouldFireForThemedControl()
		{
			int dataContextChanged = 0;
			MyViewModel dataContext = null;
			Panel c = null;
			Shown(form =>
			{
				c = new Panel();
				c.DataContextChanged += (sender, e) => dataContextChanged++;
				var expander = new CustomExpander();
				expander.Content = c;
				form.Content = expander;
				form.DataContext = dataContext = new MyViewModel();
				Assert.That(dataContextChanged, Is.EqualTo(1));
				Assert.That(c.DataContext, Is.InstanceOf<MyViewModel>());
				Assert.That(form.DataContext, Is.InstanceOf<MyViewModel>());
				Assert.That(dataContext, Is.SameAs(c.DataContext));
				Assert.That(dataContext, Is.SameAs(form.DataContext));
				Assert.That(dataContext, Is.SameAs(form.Content.DataContext));
				return form;
			}, form =>
			{
				Assert.That(dataContextChanged, Is.EqualTo(1));
				Assert.That(c?.DataContext, Is.InstanceOf<MyViewModel>());
				Assert.That(form.DataContext, Is.InstanceOf<MyViewModel>());
				Assert.That(dataContext, Is.SameAs(c.DataContext));
				Assert.That(dataContext, Is.SameAs(form.DataContext));
				Assert.That(dataContext, Is.SameAs(form.Content.DataContext));
			});
		}

		[Test]
		public void DataContextChangedShouldFireWhenSettingContentAfterLoaded()
		{
			int dataContextChanged = 0;
			int contentDataContextChanged = 0;
			MyViewModel dataContext = null;
			Shown(form =>
			{
				form.DataContextChanged += (sender, e) => dataContextChanged++;
				form.DataContext = dataContext = new MyViewModel();
				Assert.That(dataContextChanged, Is.EqualTo(1));
				Assert.That(form.DataContext, Is.InstanceOf<MyViewModel>());
				Assert.That(dataContext, Is.SameAs(form.DataContext));
				return form;
			}, form =>
			{
				var c = new Panel();
				c.DataContextChanged += (sender, e) => contentDataContextChanged++;
				form.Content = c;
				Assert.That(contentDataContextChanged, Is.EqualTo(1));
				Assert.That(dataContextChanged, Is.EqualTo(1));
				Assert.That(c.DataContext, Is.InstanceOf<MyViewModel>());
				Assert.That(form.DataContext, Is.InstanceOf<MyViewModel>());
				Assert.That(dataContext, Is.SameAs(c.DataContext));
				Assert.That(dataContext, Is.SameAs(form.DataContext));

				form.DataContext = dataContext = new MyViewModel();
				Assert.That(contentDataContextChanged, Is.EqualTo(2));
				Assert.That(dataContextChanged, Is.EqualTo(2));
				Assert.That(dataContext, Is.SameAs(c.DataContext));
				Assert.That(dataContext, Is.SameAs(form.DataContext));
			});
		}

		[Test]
		public void DataContextChangedShouldFireWhenSettingContentAfterLoadedWithThemedControl()
		{
			int dataContextChanged = 0;
			int contentDataContextChanged = 0;
			MyViewModel dataContext = null;
			Shown(form =>
			{
				form.DataContextChanged += (sender, e) => dataContextChanged++;
				form.DataContext = dataContext = new MyViewModel();
				Assert.That(dataContextChanged, Is.EqualTo(1));
				Assert.That(dataContext, Is.SameAs(form.DataContext));
				return form;
			}, form =>
			{
				var c = new Panel();
				c.DataContextChanged += (sender, e) => contentDataContextChanged++;
				form.Content = new CustomExpander { Content = c };
				Assert.That(contentDataContextChanged, Is.EqualTo(1));
				Assert.That(dataContextChanged, Is.EqualTo(1));
				Assert.That(dataContext, Is.SameAs(c.DataContext));
				Assert.That(dataContext, Is.SameAs(form.DataContext));

				form.DataContext = dataContext = new MyViewModel();
				Assert.That(contentDataContextChanged, Is.EqualTo(2));
				Assert.That(dataContextChanged, Is.EqualTo(2));
				Assert.That(dataContext, Is.SameAs(c.DataContext));
				Assert.That(dataContext, Is.SameAs(form.DataContext));
			});
		}

		[Test]
		public void DataContextChangedShouldFireForChildWithCustomModel()
		{
			int dataContextChanged = 0;
			int childDataContextChanged = 0;
			MyViewModel dataContext;
			MyViewModel childDataContext;
			Shown(form =>
			{
				var container = new Panel();
				container.DataContextChanged += (sender, e) => dataContextChanged++;
				container.DataContext = dataContext = new MyViewModel();
				Assert.That(dataContextChanged, Is.EqualTo(1));
				Assert.That(dataContext, Is.SameAs(container.DataContext));

				var child = new Panel();
				child.DataContextChanged += (sender, e) => childDataContextChanged++;
				child.DataContext = childDataContext = new MyViewModel();
				container.Content = child;
				form.Content = container;

				Assert.That(childDataContextChanged, Is.EqualTo(1));
				Assert.That(dataContext, Is.SameAs(container.DataContext));
				Assert.That(childDataContext, Is.SameAs(child.DataContext));
			}, () =>
			{
				Assert.That(dataContextChanged, Is.EqualTo(1));
				Assert.That(childDataContextChanged, Is.EqualTo(1));
			});
		}

		[Test]
		public void DataContextChangeShouldFireForControlInStackLayout()
		{
			int dataContextChanged = 0;
			MyViewModel dataContext = null;
			Panel c = null;
			Shown(form =>
			{
				c = new Panel();
				c.DataContextChanged += (sender, e) =>
					dataContextChanged++;

				form.Content = new StackLayout
				{
					Items = { c }
				};
				form.DataContext = dataContext = new MyViewModel();
				Assert.That(dataContextChanged, Is.EqualTo(1));
				Assert.That(c.DataContext, Is.Not.Null);
				Assert.That(dataContext, Is.SameAs(c.DataContext));
				Assert.That(dataContext, Is.SameAs(form.DataContext));
				return form;
			}, form =>
			{
				Assert.That(dataContextChanged, Is.EqualTo(1));
				Assert.That(c.DataContext, Is.Not.Null);
				Assert.That(dataContext, Is.SameAs(c.DataContext));
				Assert.That(dataContext, Is.SameAs(form.DataContext));
			});
		}

		[Test]
		public void DataContextChangeShouldFireForControlInTableLayout()
		{
			int dataContextChanged = 0;
			MyViewModel dataContext = null;
			Panel c = null;
			Shown(form =>
			{
				c = new Panel();
				c.DataContextChanged += (sender, e) => dataContextChanged++;

				form.Content = new TableLayout
				{
					Rows = { c }
				};
				form.DataContext = dataContext = new MyViewModel();
				Assert.That(dataContextChanged, Is.EqualTo(1));
				Assert.That(c.DataContext, Is.Not.Null);
				Assert.That(dataContext, Is.SameAs(c.DataContext));
				Assert.That(dataContext, Is.SameAs(form.DataContext));
				return form;
			}, form =>
			{
				Assert.That(dataContextChanged, Is.EqualTo(1));
				Assert.That(c.DataContext, Is.Not.Null);
				Assert.That(dataContext, Is.SameAs(c.DataContext));
				Assert.That(dataContext, Is.SameAs(form.DataContext));
			});
		}

		/// <summary>
		/// Test to ensure that the DataContextChanged event doesn't fire for child controls that already have a DataContext
		/// defined.  See issue #575.
		/// </summary>
		[Test]
		public void DataContextInSubChildShouldNotBeChangedWhenParentIsSet()
		{
			Invoke(() =>
			{
				int childChanged = 0;
				int parentChanged = 0;
				int subChildChanged = 0;
				var parent = new Panel();
				parent.DataContextChanged += (sender, e) => parentChanged++;
				parent.DataContext = new MyViewModel { ID = 1 };
				Assert.That(parentChanged, Is.EqualTo(1));

				var subChild = new Panel();
				subChild.DataContextChanged += (sender, e) => subChildChanged++;
				subChild.DataContext = new MyViewModel { ID = 2 };
				Assert.That(subChildChanged, Is.EqualTo(1));

				var child = new Panel();
				child.DataContextChanged += (sender, e) => childChanged++;
				Assert.That(childChanged, Is.EqualTo(0));
				child.Content = subChild;
				Assert.That(subChildChanged, Is.EqualTo(1));
				Assert.That(childChanged, Is.EqualTo(0));

				parent.Content = child;
				Assert.That(childChanged, Is.EqualTo(1));
				Assert.That(subChildChanged, Is.EqualTo(1));
				Assert.That(parentChanged, Is.EqualTo(1));

				Assert.That(parent.DataContext, Is.InstanceOf<MyViewModel>());
				Assert.That(parent.DataContext, Is.InstanceOf<MyViewModel>());
				Assert.That(((MyViewModel)parent.DataContext).ID, Is.EqualTo(1));
				Assert.That(child.DataContext, Is.InstanceOf<MyViewModel>());
				Assert.That(((MyViewModel)child.DataContext).ID, Is.EqualTo(1));
				Assert.That(subChild.DataContext, Is.InstanceOf<MyViewModel>());
				Assert.That(((MyViewModel)subChild.DataContext).ID, Is.EqualTo(2));
			});
		}

		[Test]
		public void DataContextWithEqualsShouldSet()
		{
			Invoke(() =>
			{
				int changed = 0;
				var panel = new Panel();
				panel.DataContextChanged += (sender, e) => changed++;

				panel.DataContext = new MyViewModelWithEquals { ID = 10 };
				Assert.That(changed, Is.EqualTo(1));

				// should be set again, even though they are equal
				panel.DataContext = new MyViewModelWithEquals { ID = 10 };
				Assert.That(changed, Is.EqualTo(2));

				panel.DataContext = new MyViewModelWithEquals { ID = 20 };
				Assert.That(changed, Is.EqualTo(3));
			});
		}

		class DropDownViewModel
		{
			public string[] DataSource { get; set; }

			public int SelectedIndex { get; set; }
		}

		[Test]
		public void ChangingDataContextShouldNotSetValues() => Shown(
			form =>
			{
				var dropDown = new DropDown();
				dropDown.BindDataContext(c => c.DataStore, (DropDownViewModel m) => m.DataSource);
				dropDown.BindDataContext(c => c.SelectedIndex, (DropDownViewModel m) => m.SelectedIndex);
				return dropDown;
			},
			dropDown =>
			{
				var model1 = new DropDownViewModel
				{
					DataSource = new string[] { "Item 1", "Item 2", "Item 3" },
					SelectedIndex = 0
				};
				dropDown.DataContext = model1;
				Assert.That(dropDown.SelectedIndex, Is.EqualTo(0), "#1");

				var model2 = new DropDownViewModel
				{
					DataSource = new string[] { "Item 4", "Item 5", "Item 6" },
					SelectedIndex = 1
				};
				dropDown.DataContext = model2;
				Assert.That(dropDown.SelectedIndex, Is.EqualTo(1), "#2");

				Assert.That(model1.SelectedIndex, Is.EqualTo(0), "#3 - Model 1 was changed");
				Assert.That(model2.SelectedIndex, Is.EqualTo(1), "#4 - Model 2 was changed");
			});
			
		[Test]
		public void RemovingFromParentShouldTriggerBindingChanged() => Invoke(() =>
		{
			int parentDataContextChanged = 0;
			int childDataContextChanged = 0;
			var panel = new Panel();
			panel.DataContextChanged += (sender, e) => parentDataContextChanged++;

			panel.DataContext = new MyViewModelWithEquals { ID = 10 };
			Assert.That(parentDataContextChanged, Is.EqualTo(1));

			var child = new Panel();
			child.DataContextChanged += (sender, e) => childDataContextChanged++;

			panel.Content = child;
			Assert.That(childDataContextChanged, Is.EqualTo(1));
			Assert.That(child.DataContext, Is.SameAs(panel.DataContext));

			panel.Content = null;
			Assert.That(childDataContextChanged, Is.EqualTo(2));
			Assert.That(parentDataContextChanged, Is.EqualTo(1));
			Assert.That(child.DataContext, Is.Null);
		});
	}
}

